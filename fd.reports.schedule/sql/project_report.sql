select 'OA' as 系统,
       c.customer_name as 使用公司,
       p.project_id as 项目编号,
       ia.attachment_code as 报告编号_使用,
       rs.report_code as 报告编号,
       p.created_time as "立项/指派时间",
       p.project_name as 项目名称,
       tu.user_name as 立项人,
       st.object_id as 标的物编号,
       st.st_object_name as 标的物名称,
       st.st_address as 标的物地址,
       fn_get_dic_name(st.st_object_type) as 标的物类型,
       fn_get_pca_name(st.st_pca_code) as 标的物区域,
       fn_get_dic_name(p.appraisal_purpose) as 评估目的,
       pe.CLIENT_NAME as 委托方名称,
       fn_get_dic_name(pe.client_type) as 委托方类型,
       p.crm_customer_name as 业务来源,
       pe.bank_business_no 银行合同编号,
       pe.external_no as 外部编号,
       pe.report_person 报告使用人,
       fn_get_dic_name(p.project_type) as 项目类型,
       fn_get_user_name(pe.Business_By) as 业务员,
       st.st_area as 标的物面积,
       ia.unit_price as 报告单价,
       ia.total_price as 报告总价,
       et.evaluate_price as 测算单价,
       et.evaluate_total_price as 测算总价,
       fn_get_user_name(st.executor_by) as 查勘执行人,
       fn_get_user_name(st.dispatcher_by) as 查勘调度人,
       st.longitude as 纬度,
       st.latitude as 经度,
       st.address as 签到地址,
       st.sign_time as 签到时间,
       st.allocate_date as 查勘调度时间,
       st.completed_date as 查勘完成时间,
       fn_get_user_name(et.dispatcher_by) as 测算调度人,
       et.allocate_date as 测算调度时间,
       fn_get_user_name(et.executor_by) as 测算执行人,
       et.completed_date as 测算完成时间,
       fn_get_user_name(est.dispatcher_by) as 预估调度人,
       est.allocate_date as 预估调度时间,
       fn_get_user_name(est.executor_by) as 预估执行人,
       est.completed_date as 预估完成时间,
       fn_get_user_name(rept.dispatcher_by) as 报告调度人,
       rept.allocate_date as 报告调度时间,
       fn_get_user_name(rept.executor_by) as 报告执行人,
       rept.completed_date as 报告完成时间,
       fn_get_user_name(esat.dispatcher_by) as 预估1审核调度人,
       esat.allocate_date as 预估1审核调度时间,
       fn_get_user_name(esat.executor_by) as 预估1审核执行人,
       esat.completed_date as 预估1审核完成时间,
       fn_get_dic_name(esat.audit_action) as 预估1审核状态,
       fn_get_user_name(esat2.dispatcher_by) as 预估2审核调度人,
       esat2.allocate_date as 预估2审核调度时间,
       fn_get_user_name(esat2.executor_by) as 预估2审核执行人,
       esat2.completed_date as 预估2审核完成时间,
       fn_get_dic_name(esat2.audit_action) as 预估2审核状态,
       fn_get_user_name(esat3.dispatcher_by) as 预估3审核调度人,
       esat3.allocate_date as 预估3审核调度时间,
       fn_get_user_name(esat3.executor_by) as 预估3审核执行人,
       esat3.completed_date as 预估3审核完成时间,
       fn_get_dic_name(esat3.audit_action) as 预估3审核状态,
       repat.allocate_date as 报告1审核调度时间,
       fn_get_user_name(repat.executor_by) as 报告1审核执行人,
       repat.completed_date as 报告1审核完成时间,
       fn_get_dic_name(repat.audit_action) as 报告1审核状态,
       repat2.allocate_date as 报告2审核调度时间,
       fn_get_user_name(repat2.executor_by) as 报告2审核执行人,
       repat2.completed_date as 报告2审核完成时间,
       fn_get_dic_name(repat2.audit_action) as 报告2审核状态,
       repat3.allocate_date as 报告3审核调度时间,
       fn_get_user_name(repat3.executor_by) as 报告3审核执行人,
       repat3.completed_date as 报告3审核完成时间,
       fn_get_dic_name(repat3.audit_action) as 报告3审核状态,
       decode(espp.action, null, '否', '是') as 预估是否盖章,
       fn_get_user_name(espp.created_by) as 预估盖章执行人,
       espp.created_time as 预估盖章执行时间,
       decode(repp.action, null, '否', '是') as 报告是否盖章,
       fn_get_user_name(repp.created_by) as 报告盖章执行人,
       repp.created_time as 报告盖章执行时间,
       fn_get_user_name(am.created_by) as 预估送达执行人,
       fn_to_day(am.created_time) 预估送达时间,
       fn_get_user_name(rm.created_by) as 报告纸质送达执行人,
       fn_to_day(rm.created_time) as 报告纸质送达时间,
       fn_get_user_name(rm2.created_by) as 报告电子送达执行人,
       fn_to_day(rm2.created_time) as 报告电子送达时间,
       fn_get_user_name(ip.apply_by) as 收费执行人,
       ip.apply_date as 收费完成时间,
       fn_get_user_name(ip.invoice_by) as 开票执行人,
       ip.invoice_complete_date as 开票完成时间,
       fn_get_user_name(pat.created_by) as 归档执行人,
       pat.created_time as 归档完成时间,
       fn_get_dic_name(p.project_status) as 项目状态,
       pm.remark 备注,
       fn_get_dic_name(p.priority_level) as 优先级,
       p.survey_date as 看房日期,
       fn_get_dic_name(p.survey_time) as 看房时间段,
       esat.audit_remark as 预估1审核意见,
       esat2.audit_remark as 预估2审核意见,
       esat3.audit_remark as 预估3审核意见,
       repat.audit_remark as 报告1审核意见,
       repat2.audit_remark as 报告2审核意见,
       repat3.audit_remark as 报告3审核意见,
       fn_get_user_name(nt.executor_by) as 撰写执行人,
       nt.completed_date as 撰写完成时间,
       fn_get_user_name(nat.executor_by) as 撰写审核执行人,
       nat.completed_date as 撰写审核完成时间,
       fn_get_user_name(it.executor_by) as 询价单执行人,
       it.completed_date as 询价单完成时间,
       fn_get_user_name(iat.executor_by) as 询价单审核执行人,
       iat.completed_date as 询价单审核完成时间,
       fn_get_dic_name(iat.audit_action) as 询价单审核状态,
       iat.audit_remark as 询价单审核意见,
       fn_get_dic_name(pe.project_type) as 银行项目类型,
       fn_get_dic_name(pe.loan_type) as 贷款品种,
       pe.contact_person as 业务经理,
       pe.contact_phone as 业务经理电话,
       pe.property_owner as 客户姓名,
       pe.SUPPOSED_FEE as 业务计费金额,
       fn_get_user_name(ft.executor_by) as 业务计费执行人,
       ft.completed_date as 业务计费完成时间,
       pe.REVIEW_FEE as 运保计费金额,
       fn_get_user_name(ft2.executor_by) as 运保计费执行人,
       ft2.completed_date as 运保计费完成时间,
       pe.adjuest_fee 银行调整计费金额,
       fn_get_user_name(ft3.executor_by) as 银行调整计费执行人,
       ft3.completed_date as 银行调整计费完成时间,
       p.payment_fee as 应收金额,
       fn_get_dic_name(ip.receipt_status) as 回款状态,
       fn_get_user_name(p.record_by) as 备案人,
       st.task_id as 事项ID
  from t_project p
  left join t_project_expand pe
    on pe.project_id = p.project_id
  left join ompd.t_user tu
    on p.created_by = tu.user_id
--查勘事项
  left join (select t.*,
                    t1.survey_object_id,
                    o.object_name st_object_name,
                    o.address st_address,
                    o.pca_code st_pca_code,
                    o.area st_area,
                    o.object_type st_object_type,
                    o.object_id,
                    t1.longitude,
                    t1.latitude,
                    t1.address,
                    t1.sign_time,
                    row_number() over(partition by t1.survey_object_id order by t1.task_id desc) rn
               from t_task t
              inner join t_survey_task t1
                 on t1.task_id = t.task_id
                and t.is_delete = 0
                and t.task_type = 40057002
               left join t_object o
                 on o.object_id = t1.survey_object_id) st
    on st.project_id = p.project_id
   and st.rn = 1
--测算事项
  left join (select t.*,
                    tr.rel_task_id,
                    t1.evaluate_price,
                    t1.evaluate_total_price,
                    t1.evaluate_object_id,
                    row_number() over(partition by t1.evaluate_object_id order by t1.task_id desc) rn
               from t_task t
              inner join t_evaluate_task t1
                 on t1.task_id = t.task_id
                and t.is_delete = 0
                and t.task_type = 40057003
               left join oa.t_task_relation tr
                 on tr.task_id = t.task_id) et
    on et.project_id = p.project_id
   and st.task_id = et.rel_task_id
--撰写事项
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag = 40053003) it
    on it.project_id = p.project_id
   and it.rn = 1
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag is null) nt
    on nt.project_id = p.project_id
   and nt.rn = 1
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag = 40053001) est
    on est.project_id = p.project_id
   and est.rn = 1
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
--报告编号
  left join (select rs.project_id,
                    listagg(to_char(rs.sequence_value), ',') within group(order by rs.report_sequence_id) as report_code
               from oa.t_report_sequence rs
              where (rs.is_delete = 0 or rs.is_delete is null)
              group by rs.project_id) rs
    on rs.project_id = p.project_id
--报告文件
  left join (select pi.project_id,
                    a.unit_price,
                    a.attachment_code,
                    a.total_price,
                    a.total_area,
                    row_number() over(partition by t.project_id order by a.info_attachment_id desc) rn
               from t_project_info pi
              inner join t_task t
                 on t.task_id = pi.task_id
                and t.task_type = 40057004
              inner join t_report_task rt
                 on rt.task_id = t.task_id
              inner join t_info_attachment a
                 on pi.project_info_id = a.project_info_id
              where a.is_delete is null
                 or a.is_delete <> 1) ia
    on ia.project_id = p.project_id
   and ia.rn = 1
--审核事项
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053003) iat
    on iat.project_id = p.project_id
   and iat.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag is null) nat
    on nat.project_id = p.project_id
   and nat.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053001
                and t1.audit_link = 1) esat
    on esat.project_id = p.project_id
   and esat.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053001
                and t1.audit_link = 2) esat2
    on esat2.project_id = p.project_id
   and esat2.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053001
                and t1.audit_link = 3) esat3
    on esat3.project_id = p.project_id
   and esat3.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053002
                and t1.audit_link = 1) repat
    on repat.project_id = p.project_id
   and repat.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053002
                and t1.audit_link = 2) repat2
    on repat2.project_id = p.project_id
   and repat2.rn = 1
  left join (select t.*,
                    t1.audit_action,
                    t1.audit_remark,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_audit_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057005
                and t.is_delete = 0
              inner join t_task_relation tr
                 on tr.task_id = t1.task_id
              inner join t_report_task t2
                 on t2.task_id = tr.rel_task_id
                and t2.report_flag = 40053002
                and t1.audit_link = 3) repat3
    on repat3.project_id = p.project_id
   and repat3.rn = 1
--预估盖章
  left join (select i.project_id,
                    ar.action,
                    ar.created_time,
                    ar.created_by,
                    row_number() over(partition by i.project_id order by ar.created_time desc) rn
               from t_project_info i
              inner join t_task t
                 on t.task_id = i.task_id
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag = 40053001
              inner join t_info_attachment a
                 on a.project_info_id = i.project_info_id
              inner join t_attachment_record ar
                 on ar.info_attachment_id = a.info_attachment_id
              where ar.action = 40060003) espp
    on espp.project_id = p.project_id
   and espp.rn = 1
--报告盖章
  left join (select i.project_id,
                    ar.action,
                    ar.created_time,
                    ar.created_by,
                    row_number() over(partition by i.project_id order by ar.created_time desc) rn
               from t_project_info i
              inner join t_task t
                 on t.task_id = i.task_id
              inner join t_report_task t1
                 on t1.task_id = t.task_id
                and t.task_type = 40057004
                and t.is_delete = 0
                and t1.report_flag = 40053002
              inner join t_info_attachment a
                 on a.project_info_id = i.project_info_id
              inner join t_attachment_record ar
                 on ar.info_attachment_id = a.info_attachment_id
              where ar.action = 40060003) repp
    on repp.project_id = p.project_id
   and repp.rn = 1
-- 预估送达
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
                and t3.report_flag = 40053001
               left join t_task t4
                 on t3.task_id = t4.task_id
              where t.mail_method = 40068001) am
    on am.project_id = p.project_id
   and am.rn = 1
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
--收费2
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_finance_task ft
                 on ft.task_id = t.task_id
                and t.task_type = 40057008
                and t.is_delete = 0
                and ft.finance_link = '2') ft2
    on ft2.project_id = p.project_id
--收费3
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_task t
              inner join t_finance_task ft
                 on ft.task_id = t.task_id
                and t.task_type = 40057008
                and t.is_delete = 0
                and ft.finance_link = '3') ft3
    on ft3.project_id = p.project_id
--开票
  left join (select iap.receipt_status,
                    iap.apply_by,
                    iap.apply_date,
                    pr.project_id,
                    iap.invoice_by,
                    iap.invoice_complete_date
               from oa.t_invoice_apply iap
              inner join oa.t_invoice_project_ref pr
                 on iap.invoice_apply_id = pr.invoice_apply_id) ip
    on p.project_id = ip.project_id
--归档   
  left join (select t.*,
                    row_number() over(partition by t.project_id order by t.created_time desc) rn
               from t_project_archive t) pat
    on pat.project_id = p.project_id
   and pat.rn = 1
--备注
  left join (select m.project_id,
                    listagg(to_char(m.remark_content), ',') within group(order by project_id) remark
               from t_project_remark m
              group by m.project_id) pm
    on pm.project_id = p.project_id
  left join ompd.t_customer c
    on c.customer_id = p.customer_id
 where p.customer_id in (389,886)
   and p.is_delete = 0
   and p.project_status in (40001001, 40001002, 40001003, 40001005)     
      and p.created_time >= to_date('2024-01-01 00:00:00', 'yyyy-MM-dd hh24:mi:ss')
         -- to_date('2024-01-01 00:00:00', 'yyyy-MM-dd hh24:mi:ss')   
 order by p.created_time