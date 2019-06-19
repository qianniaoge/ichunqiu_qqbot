using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools
{
    /// <summary>
    /// MySQL数据库管理工具类
    /// </summary>
    class ConnectionPool
    {
        // 池管理对象
        private static ConnectionPool connectionPool = null;
        // 池管理对象实例类
        private static Object objlock = typeof(ConnectionPool);
        // 池中连接数
        private int size = 100;
        // 链接保存集合
        private ArrayList pool = null;
        // 已经使用的连接数
        private int useCount = 0;
        // 数据库链接字符串
        private String connectionStr = "";
        /// <summary>
        /// 无参构造
        /// </summary>
        public ConnectionPool() {
            connectionStr = Constants.connectionStr;
            // 创建可用链接集合
            pool = new ArrayList();
        }

        /// <summary>
        /// 获取数据库连接池
        /// </summary>
        /// <returns></returns>
        public static ConnectionPool getPool() {
            lock (objlock)
            {
                if (connectionPool == null)
                {
                    connectionPool = new ConnectionPool();
                }
                return connectionPool;
            }
        }

        /// <summary>
        /// 获取数据库链接对象
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getConnection() {
            lock (pool)
            {
                MySqlConnection mySqlConnection = null;
                if (pool.Count > 0)
                {
                    mySqlConnection = (MySqlConnection)pool[0];
                    mySqlConnection.Open();
                    //  在可用连接中移除此链接
                    pool.RemoveAt(0);
                    // 不成功
                    if (isUserful(mySqlConnection))
                    {
                        // 可用的连接数据已去掉一个
                        useCount--;
                        mySqlConnection = getConnection();
                    }
                }
                else
                {
                    // 可用链接小于链接数量
                    if (useCount <= size)
                    {
                        try
                        {
                            MySqlConnection conn = new MySqlConnection(connectionStr);
                            conn.Open();
                            // 可用链接加1
                            useCount++;
                            mySqlConnection = conn;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                return mySqlConnection;
            }
        }

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        /// <param name="conn"></param>
        public void closeConnection(MySqlConnection conn) {

            lock (pool)
            {
                if (conn != null)
                {
                    // 将次链接放入连接池中
                    pool.Add(conn);
                }
            }
        }

        /// <summary>
        /// 测试数据库链接可用
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private bool isUserful(MySqlConnection conn) {
            bool result = true;
            if (conn != null)
            {
                // 测试sql
                string sql = "SELECT 1;";
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
