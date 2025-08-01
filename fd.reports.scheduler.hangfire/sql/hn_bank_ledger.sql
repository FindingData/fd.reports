select p.project_id 项目编号,
       ' ' as "机构（长沙分行）",
       '' as "客户名称（借款人）",
       ' ' as "企业类型",
       pe.report_person as 所属机构,
       ' ' as 客户经理,
       ' ' as 业务归属条线,
       ' ' as "贷款申请日",
       ' ' as 抵押合同编号,
       ' ' as 合同日期,
       ' ' as "合同金额（万元）",
       ' ' as 报销金额,
       ' ' as 报销日期,
       ' ' as "评估报告类型",
       ' ' as 分行审查人,
       '湖南经典房地产评估咨询有限公司' as 评估公司名称,
       trim(pe.property_owner) 抵押人,
       round(ia.total_price / 10000, 4) "评估物价值（元）",
       to_char(ia.reported_date, 'yyyy-MM-dd') 正式评估报告出具日期,
       p.Payable_Fee "评估费金额（元）",
       pe.CONTACT_PERSON 客户经理姓名,
       
       decode(p.is_invoice,1,'是','否') as 是否报账,
       ' ' as 报销金额,
       ' ' 记账日期,
       ' ' 记账凭证号,
       ' ' 是否需要整改,
       ' ' 自查整改措施,
       ' ' 自查整改完成时限,
       ' ' 自查整改责任人,
       ' ' 备注,
       p.project_name 项目名称,
       rs.report_code 评估报告编号,
       fn_get_user_name(ft.executor_by) as 业务计费执行人,
       pe.supposed_fee 业务计费金额,
       pe.review_fee 运保复核金额,
       p.project_id
  from t_project p
  left join t_project_expand pe
    on p.project_id = pe.project_id
--标的物地址
  left join (select o.address,
                    o.property_owner,
                    count(0) over(partition by pr.project_id order by pr.object_id desc) object_count,
                    o.object_type || lead(',' || o.object_type, 1) over(partition by pr.project_id order by pr.object_id desc) object_type,
                    pr.project_id,
                    row_number() over(partition by pr.project_id order by pr.object_id desc) rn
               from t_object o
               left join t_project_object_ref pr
                 on o.object_id = pr.object_id) po
    on po.project_id = p.project_id
   and po.rn = 1
--报告文件
  left join (select pi.project_id,
                    a.unit_price,
                    a.attachment_code,
                    a.total_price,
                    a.total_area,
                    a.reported_date,
                    row_number() over(partition by t.project_id order by a.info_attachment_id desc) rn
               from t_project_info pi
              inner join t_task t
                 on t.task_id = pi.task_id
                and t.task_type = 40057004
              inner join t_report_task rt
                 on rt.task_id = t.task_id
                and rt.report_flag = 40053002
              inner join t_info_attachment a
                 on pi.project_info_id = a.project_info_id
              where (a.is_delete is null or a.is_delete <> 1)) ia
    on ia.project_id = p.project_id
   and ia.rn = 1
--报告编号,
  left join (select rs.project_id,
                    listagg(to_char(rs.sequence_value), ',') within group(order by rs.report_sequence_id) as report_code
               from oa.t_report_sequence rs
              where (rs.is_delete = 0 or rs.is_delete is null)
              group by rs.project_id) rs
    on rs.project_id = p.project_id
---报告撰写
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag = 40053002) rept
    on rept.project_id = p.project_id
   and rept.rn = 1
--报告纸质送达
  left join (select t.created_time,
                    t.created_by,
                    t4.project_id,
                    row_number() over(partition by t4.project_id order by t.attachment_mail_id desc) rn
               from t_attachment_mail t
               left join t_info_attachment t1
                 on t.info_attachment_id = t1.info_attachment_id
               left join t_project_info t2
                 on t1.project_info_id = t2.project_info_id
               left join t_report_task t3
                 on t3.task_id = t2.task_id
                and t3.report_flag = 40053002
               left join t_task t4
                 on t3.task_id = t4.task_id
              where t.mail_method = 40068001
                and t.send_mode = 40069001) rm
    on rm.project_id = p.project_id
   and rm.rn = 1
--报告电子送达
  left join (select t.created_time,
                    t.created_by,
                    t4.project_id,                    
                    row_number() over(partition by t4.project_id order by t.attachment_mail_id desc) rn
               from t_attachment_mail t
               left join t_info_attachment t1
                 on t.info_attachment_id = t1.info_attachment_id
               left join t_project_info t2
                 on t1.project_info_id = t2.project_info_id
               left join t_report_task t3
                 on t3.task_id = t2.task_id
                and t3.report_flag = 40053002
               left join t_task t4
                 on t3.task_id = t4.task_id
              where t.mail_method = 40068001
                and t.send_mode = 40069002) rm2
    on rm2.project_id = p.project_id
   and rm2.rn = 1
--收费1
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_finance_task ft
                 on ft.task_id = t.task_id
                and t.task_type = 40057008
                and t.is_delete = 0
                and ft.finance_link = '1') ft
    on ft.project_id = p.project_id
   and ft.rn = 1
 where p.is_delete = 0
   and (rm.created_time >= :report_date_start and
       rm.created_time <= :report_date_end or
       (rm2.created_time >=
       :report_date_start and
       rm2.created_time <=
       :report_date_end) or
       (rept.completed_date >=
       :report_date_start and
       rept.completed_date <=
      :report_date_end))
   and p.customer_id = 389