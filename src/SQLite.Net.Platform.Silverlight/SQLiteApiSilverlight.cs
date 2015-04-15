using System;
using System.Runtime.InteropServices;
using SQLite.Net.Interop;
using System.Text;

using Sqlite3DatabaseHandle = System.IntPtr;
using Sqlite3Statement = System.IntPtr;

namespace SQLite.Net.Platform.Silverlight
{
    public class SQLiteApiSilverlight : ISQLiteApiExt
    {
        public SQLiteApiSilverlight()
        {
        }

        public int BindBlob(IDbStatement stmt, int index, byte[] val, int n, IntPtr free)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindBlob(dbStatement.InternalStmt, index, val, n, free);
        }

        public int BindDouble(IDbStatement stmt, int index, double val)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindDouble(dbStatement.InternalStmt, index, val);
        }

        public int BindInt(IDbStatement stmt, int index, int val)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindInt(dbStatement.InternalStmt, index, val);
        }

        public int BindInt64(IDbStatement stmt, int index, long val)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindInt64(dbStatement.InternalStmt, index, val);
        }

        public int BindNull(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindNull(dbStatement.InternalStmt, index);
        }

        public int BindParameterIndex(IDbStatement stmt, string name)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindParameterIndex(dbStatement.InternalStmt, name);
        }

        public int BindText16(IDbStatement stmt, int index, string val, int n, IntPtr free)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.BindText(dbStatement.InternalStmt, index, val, n, free);
        }

        public Result BusyTimeout(IDbHandle db, int milliseconds)
        {
            var dbHandle = (DbHandle)db;
            return (Result)SQLite3.BusyTimeout(dbHandle.InternalDbHandle, milliseconds);
        }

        public int Changes(IDbHandle db)
        {
            var dbHandle = (DbHandle)db;
            return SQLite3.Changes(dbHandle.InternalDbHandle);
        }

        public Result Close(IDbHandle db)
        {
            var dbHandle = (DbHandle)db;
            return (Result)SQLite3.Close(dbHandle.InternalDbHandle);
        }

        public Result Initialize()
        {
            throw new NotSupportedException();
        }
        public Result Shutdown()
        {
            throw new NotSupportedException();
        }

        public Result Config(ConfigOption option)
        {
            return (Result)SQLite3.Config(option);
        }


        public byte[] ColumnBlob(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            int length = ColumnBytes(stmt, index);
            byte[] result = new byte[length];
            if (length > 0)
                Marshal.Copy(SQLite3.ColumnBlob(dbStatement.InternalStmt, index), result, 0, length);
            return result;
        }

        public byte[] ColumnByteArray(IDbStatement stmt, int index)
        {
            return ColumnBlob(stmt, index);
        }

        public int ColumnBytes(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnBytes(dbStatement.InternalStmt, index);
        }

        public int ColumnCount(IDbStatement stmt)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnCount(dbStatement.InternalStmt);
        }

        public double ColumnDouble(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnDouble(dbStatement.InternalStmt, index);
        }

        public int ColumnInt(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnInt(dbStatement.InternalStmt, index);
        }

        public long ColumnInt64(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnInt64(dbStatement.InternalStmt, index);
        }

        public string ColumnName16(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return SQLite3.ColumnName16(dbStatement.InternalStmt, index);
        }

        public string ColumnText16(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return Marshal.PtrToStringUni(SQLite3.ColumnText16(dbStatement.InternalStmt, index));
        }

        public ColType ColumnType(IDbStatement stmt, int index)
        {
            var dbStatement = (DbStatement)stmt;
            return (ColType)SQLite3.ColumnType(dbStatement.InternalStmt, index);
        }

        public int LibVersionNumber()
        {
            return SQLite3.sqlite3_libversion_number();
        }

        public Result EnableLoadExtension(IDbHandle db, int onoff)
        {
            return (Result)1;
        }

        public string Errmsg16(IDbHandle db)
        {
            var dbHandle = (DbHandle)db;
            return SQLite3.GetErrmsg(dbHandle.InternalDbHandle);
        }

        public Result Finalize(IDbStatement stmt)
        {
            var dbStatement = (DbStatement)stmt;
            Sqlite3Statement internalStmt = dbStatement.InternalStmt;
            return (Result)SQLite3.Finalize(internalStmt);
        }

        public long LastInsertRowid(IDbHandle db)
        {
            var dbHandle = (DbHandle)db;
            return SQLite3.LastInsertRowid(dbHandle.InternalDbHandle);
        }

        public Result Open(byte[] filename, out IDbHandle db, int flags, IntPtr zvfs)
        {
            Sqlite3DatabaseHandle internalDbHandle;
            var ret = (Result)SQLite3.Open(filename, out internalDbHandle, flags, zvfs);
            db = new DbHandle(internalDbHandle);
            return ret;
        }

        public ExtendedResult ExtendedErrCode(IDbHandle db)
        {
            var dbHandle = (DbHandle)db;
            return SQLite3.sqlite3_extended_errcode(dbHandle.InternalDbHandle);
        }

        public IDbStatement Prepare2(IDbHandle db, string query)
        {
            var dbHandle = (DbHandle)db;
            var stmt = default(Sqlite3Statement);
            var r = SQLite3.Prepare2(dbHandle.InternalDbHandle, query, query.Length, out stmt, IntPtr.Zero);
            if (r != Result.OK)
            {
                throw SQLiteException.New(r, SQLite3.GetErrmsg(dbHandle.InternalDbHandle));
            }
            return new DbStatement(stmt);
        }

        public Result Reset(IDbStatement stmt)
        {
            var dbStatement = (DbStatement)stmt;
            return (Result)SQLite3.Reset(dbStatement.InternalStmt);
        }

        public Result Step(IDbStatement stmt)
        {
            var dbStatement = (DbStatement)stmt;
            return (Result)SQLite3.Step(dbStatement.InternalStmt);
        }

        #region Backup

        public IDbBackupHandle BackupInit(IDbHandle destHandle, string destName, IDbHandle srcHandle, string srcName)
        {
            var internalDestDb = (DbHandle)destHandle;
            var internalSrcDb = (DbHandle)srcHandle;

            IntPtr p = SQLite3.sqlite3_backup_init(internalDestDb.InternalDbHandle,
                                                                  destName,
                                                                  internalSrcDb.InternalDbHandle,
                                                                  srcName);

            if (p == IntPtr.Zero)
            {
                return null;
            }
            else
            {
                return new DbBackupHandle(p);
            }
        }

        public Result BackupStep(IDbBackupHandle handle, int pageCount)
        {
            var internalBackup = (DbBackupHandle)handle;
            return SQLite3.sqlite3_backup_step(internalBackup.DbBackupPtr, pageCount);
        }

        public Result BackupFinish(IDbBackupHandle handle)
        {
            var internalBackup = (DbBackupHandle)handle;
            return SQLite3.sqlite3_backup_finish(internalBackup.DbBackupPtr);
        }

        public int BackupRemaining(IDbBackupHandle handle)
        {
            var internalBackup = (DbBackupHandle)handle;
            return SQLite3.sqlite3_backup_remaining(internalBackup.DbBackupPtr);
        }

        public int BackupPagecount(IDbBackupHandle handle)
        {
            var internalBackup = (DbBackupHandle)handle;
            return SQLite3.sqlite3_backup_pagecount(internalBackup.DbBackupPtr);
        }

        public int Sleep(int millis)
        {
            return SQLite3.sqlite3_sleep(millis);
        }

        private struct DbBackupHandle : IDbBackupHandle
        {
            public DbBackupHandle(IntPtr dbBackupPtr)
                : this()
            {
                DbBackupPtr = dbBackupPtr;
            }

            internal IntPtr DbBackupPtr { get; set; }

            public bool Equals(IDbBackupHandle other)
            {
                return other is DbBackupHandle && DbBackupPtr == ((DbBackupHandle)other).DbBackupPtr;
            }
        }

        #endregion

        private struct DbHandle : IDbHandle
        {
            public DbHandle(Sqlite3DatabaseHandle internalDbHandle)
                : this()
            {
                InternalDbHandle = internalDbHandle;
            }

            public Sqlite3DatabaseHandle InternalDbHandle { get; set; }

            public bool Equals(IDbHandle other)
            {
                return other is DbHandle && InternalDbHandle == ((DbHandle)other).InternalDbHandle;
            }
        }

        private struct DbStatement : IDbStatement
        {
            public DbStatement(Sqlite3Statement internalStmt)
                : this()
            {
                InternalStmt = internalStmt;
            }

            internal Sqlite3Statement InternalStmt { get; set; }

            public bool Equals(IDbStatement other)
            {
                return (other is DbStatement) && ((DbStatement)other).InternalStmt == InternalStmt;
            }
        }
    }
}
