/** 
 * star
*/
using System;
namespace Pkurg.PWorldBPM.Business.BIZ.ERP
{
	[Serializable]
	public partial class SupplementalAgreementInfo
	{
		public SupplementalAgreementInfo()
		{}
		#region Model
		private string _formid;
		private string _erpformid;
		private string _erpformtype;
		private string _startdeptid;
		private int? _isreporttoresource;
		private int? _isreporttofounder;
		private string _relationcontract;
		private string _createtime;
		private string _approveresult;
		/// <summary>
		/// 
		/// </summary>
		public string FormID
		{
			set{ _formid=value;}
			get{return _formid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErpFormId
		{
			set{ _erpformid=value;}
			get{return _erpformid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ErpFormType
		{
			set{ _erpformtype=value;}
			get{return _erpformtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StartDeptId
		{
			set{ _startdeptid=value;}
			get{return _startdeptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsReportToResource
		{
			set{ _isreporttoresource=value;}
			get{return _isreporttoresource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsReportToFounder
		{
			set{ _isreporttofounder=value;}
			get{return _isreporttofounder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RelationContract
		{
			set{ _relationcontract=value;}
			get{return _relationcontract;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ApproveResult
		{
			set{ _approveresult=value;}
			get{return _approveresult;}
		}
		#endregion Model

	}
}

