using System;
using System.Data;
using System.IO;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;

namespace SageCRM.AspNet
{
    public class SageCRMDataViewReader : IDataReader
    {
            DataView _source;
            IEnumerator _enum;
            bool _closed = true;

            public SageCRMDataViewReader(DataView source)
            {
                _source = source;
            }

            DataRowView current
            {
                get
                {
                    return (DataRowView)_enum.Current;
                }
            }

            #region IDataReader Members

            public void Close()
            {
                _closed = true;
                return;
            }

            public int Depth
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            public DataTable GetSchemaTable()
            {
                return _source.Table;
            }

            public bool IsClosed
            {
                get { return _closed; }
            }

            public bool NextResult()
            {
                return false;
            }

            public bool Read()
            {
                if (_closed)
                    _enum = _source.GetEnumerator();

                _closed = false;
                return _enum.MoveNext();
            }

            public int RecordsAffected
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                _source.Dispose();
            }

            #endregion

            #region IDataRecord Members

            public int FieldCount
            {
                get { return _source.Table.Columns.Count; }
            }

            public bool GetBoolean(int i)
            {
                return (bool)current[i];
            }

            public byte GetByte(int i)
            {
                return (byte)current[i];
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public char GetChar(int i)
            {
                return (char)current[i];
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public IDataReader GetData(int i)
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public string GetDataTypeName(int i)
            {
                return current[i].GetType().Name;
            }

            public DateTime GetDateTime(int i)
            {
                return (DateTime)current[i];
            }

            public decimal GetDecimal(int i)
            {
                return (Decimal)current[i];
            }

            public double GetDouble(int i)
            {
                return (Double)current[i];
            }

            public Type GetFieldType(int i)
            {
                return current[i].GetType();
            }

            public float GetFloat(int i)
            {
                return (float)current[i];
            }

            public Guid GetGuid(int i)
            {
                return (Guid)current[i];
            }

            public short GetInt16(int i)
            {
                return (short)current[i];
            }

            public int GetInt32(int i)
            {
                return (int)current[i];
            }

            public long GetInt64(int i)
            {
                return (long)current[i];
            }

            public string GetName(int i)
            {
                return _source.Table.Columns[i].ColumnName;
            }

            public int GetOrdinal(string name)
            {

                return _source.Table.Columns[name].Ordinal;
            }

            public string GetString(int i)
            {

                return (string)current[i];
            }

            public object GetValue(int i)
            {
                return current[i];
            }

            public int GetValues(object[] values)
            {
                int count = Math.Min(values.Length, FieldCount);

                for (int i = 0; i < count; i++)
                    values[i] = current[i];

                return count;
            }

            public bool IsDBNull(int i)
            {
                return current[i] == DBNull.Value;
            }

            public object this[string name]
            {
                get { return current[name]; }
            }

            public object this[int i]
            {
                get { return current[i]; }
            }

            #endregion

    }
}
