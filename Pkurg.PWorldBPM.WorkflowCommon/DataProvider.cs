using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Pkurg.PWorldTemp.WorkflowCommon
{
	public class DataProvider
	{
		// 数据库连接字符串
		public string ConnectionString = string.Empty;

		/// <summary>SQLSERVER数据连接字符串</summary>
		private SqlConnection DbConnection = null;
		/// <summary>SQLSERVER命令集</summary>
		private SqlCommand DbCommand = null;
		/// <summary>SQLSERVER事务</summary>
		private SqlTransaction DbTransaction = null;

		/// <summary>
		/// 启动数据库连接
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Open()
		{
			// 数据库连接对象不存在时 创建连接对象
			if (this.DbConnection == null)
			{
				this.DbConnection = new SqlConnection(ConnectionString);
			}

			// 数据库连接状态为未连接状态时 启动连接
			if (!this.DbConnection.State.Equals(ConnectionState.Open))
			{
				// 打开数据库连接
				this.DbConnection.Open();
			}
		}

		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Close()
		{
			// 数据库连接对象不存在时 退出处理
			if (this.DbConnection == null)
			{
				return;
			}

			// 数据库连接状态为连接状态时 关闭连接
			if (this.DbConnection.State.Equals(ConnectionState.Open))
			{
				this.Rollback();
				this.DbConnection.Close();
			}

			// SQLSERVER命令集
			if (this.DbCommand != null)
			{
				this.DbCommand.Dispose();
				this.DbCommand = null;
			}

			// 释放数据库连接
			this.DbConnection.Dispose();
			this.DbConnection = null;
		}

		/// <summary>
		/// 创建SqlCommand对象
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		private void CreateCommand()
		{
			// 开启数据库连接
			this.Open();

			// SqlCommand对象为空时 创建对象
			if (this.DbCommand == null)
			{
				this.DbCommand = this.DbConnection.CreateCommand();
                this.DbCommand.CommandTimeout = 180;
			}
		}

		/// <summary>
		/// 开启事务
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BeginTransaction()
		{
			// 创建SqlCommand对象
			this.CreateCommand();
			// 开启事务
			this.DbTransaction = this.DbConnection.BeginTransaction();
			// 设置SqlCommand事务
			this.DbCommand.Transaction = this.DbTransaction;
		}

		/// <summary>
		/// 提交事务
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Commit()
		{
			// 事务对象不存在时 退出处理
			if (this.DbTransaction == null)
			{
				return;
			}

			// 提交事务
			this.DbTransaction.Commit();

			// 释放事务对象
			this.DbTransaction.Dispose();
			this.DbTransaction = null;
		}

		/// <summary>
		/// 回滚事务
		/// </summary>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Rollback()
		{
			// 事务对象不存在时 退出处理
			if (this.DbTransaction == null)
			{
				return;
			}

			// 回滚事务
			this.DbTransaction.Rollback();

			// 释放事务对象
			this.DbTransaction.Dispose();
			this.DbTransaction = null;
		}

		/// <summary>
		/// 执行数据库更新SQL
		/// </summary>
		/// <param name="SQL">更新用SQL</param>
		/// <param name="type">执行类型</param>
		/// <returns>
		/// >=0:更新数据数量　-1:错误
		/// </returns>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		public int ExecuteNonQuery(string SQL, CommandType type)
		{
			// 创建SqlCommand对象
			this.CreateCommand();

			// 设置SQL
			this.DbCommand.CommandText = SQL;
			// 设置命令类型
			this.DbCommand.CommandType = type;

			// 执行更新 并返回更新数据数量
			return this.DbCommand.ExecuteNonQuery();
		}



        /// <summary>
        /// 执行数据查询
        /// </summary>
        /// <param name="SQL">查询用SQL</param>
        /// <param name="type">执行类型</param>
        /// <returns>
        /// 查询数据集
        /// </returns>
        /// <history>
        /// Created By Tony.Wang 2009/12/24
        /// </history>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public DataSet ExecuteDataSet(string SQL, CommandType type)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            try
            {
                // 创建SqlCommand对象
                this.CreateCommand();

                // 设置SQL
                this.DbCommand.CommandText = SQL;
                // 设置命令类型
                this.DbCommand.CommandType = type;

                // 创建SqlDataAdapter对象
                dataAdapter = new SqlDataAdapter(this.DbCommand);

                // 创建DataSet对象
                DataSet dataSet = new DataSet();

                // 填充DataSet
                dataAdapter.Fill(dataSet);

                // 返回查询数据集
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 释放SqlDataAdapter
                if (dataAdapter != null) dataAdapter.Dispose();
            }
        }

		/// <summary>
		/// 执行数据查询
		/// </summary>
		/// <param name="SQL">查询用SQL</param>
		/// <param name="type">执行类型</param>
		/// <returns>
		/// 查询数据集
		/// </returns>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
        public DataTable ExecutedProcedure(string strProcedureName, SqlParameter[] parameters)
		{
			SqlDataAdapter dataAdapter = new SqlDataAdapter();

			try
			{
				// 创建SqlCommand对象
				this.CreateCommand();

				// 设置SQL
                this.DbCommand.CommandText = strProcedureName;
				// 设置命令类型
                this.DbCommand.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    for (int index = 0; index < parameters.Length; index++)
                        this.DbCommand.Parameters.Add(parameters[index]);
                }

				// 创建SqlDataAdapter对象
				dataAdapter = new SqlDataAdapter(this.DbCommand);

				// 创建DataSet对象
				DataSet dataSet = new DataSet();

				// 填充DataSet
				dataAdapter.Fill(dataSet);

				// 返回查询数据集
                return dataSet.Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				// 释放SqlDataAdapter
				if (dataAdapter != null) dataAdapter.Dispose();
			}
		}

		/// <summary>
		/// 执行数据查询
		/// </summary>
		/// <param name="SQL">查询用SQL</param>
		/// <param name="type">执行类型</param>
		/// <returns>
		/// 查询数据集
		/// </returns>
		/// <history>
		/// Created By Tony.Wang 2009/12/24
		/// </history>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DataTable ExecuteDataTable(string SQL, CommandType type)
		{
			// 执行数据查询
			DataSet dataSet = this.ExecuteDataSet(SQL, type);

			// 为查询到数据时
			if (dataSet == null || dataSet.Tables.Count <= 0)
			{
				return null;
			}
			else
			{
				return dataSet.Tables[0];
			}
		}


        /// <summary>
        /// 执行数据查询获取结果集的第一行第一列
        /// </summary>
        /// <param name="SQL">查询用SQL</param>
        /// <param name="type">执行类型</param>
        /// <returns>
        /// 查询数据集的第一行第一列的值
        /// </returns>
        /// <history>
        /// Created By Qu Yanjie 2010/07/15
        /// </history>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string ExecuteScalar(string sql, CommandType type,SqlParameter[] parameters)
        {
            string returnValue = string.Empty;
            try
            {
                this.CreateCommand();
                this.DbCommand.CommandText = sql;
                this.DbCommand.CommandType = type;
                if (parameters != null)
                {
                    for(int index = 0; index < parameters.Length; index ++)
                        this.DbCommand.Parameters.Add(parameters[index]);
                }
                object objReturn = this.DbCommand.ExecuteScalar();
                if (objReturn != null)
                    returnValue = objReturn.ToString();
            }
            catch(Exception exc)
            {
                string aa = exc.Message;
            }
            return returnValue;
        }

	}
}
