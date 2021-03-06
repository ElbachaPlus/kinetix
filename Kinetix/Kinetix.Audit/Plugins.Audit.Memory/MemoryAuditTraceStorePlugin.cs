﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Kinetix.Audit
{
    public class MemoryAuditTraceStorePlugin : IAuditTraceStorePlugin
    {

        private IDictionary<long?, AuditTrace> inMemoryStore = new ConcurrentDictionary<long?, AuditTrace>();
        private int memorySequenceGenerator = 0;

        public AuditTrace ReadTrace(long? idAuditTrace)
        {
            return inMemoryStore[idAuditTrace];
        }
        
        public void CreateTrace(AuditTrace auditTrace)
        {
            Debug.Assert(auditTrace != null);
            Debug.Assert(auditTrace.Id == null, "A new audit trail must not have an id");
            //---
            int generatedId = Interlocked.Increment(ref memorySequenceGenerator);
            auditTrace.Id = generatedId;
            inMemoryStore[generatedId] = auditTrace;
        }

        public IList<AuditTrace> FindTraceByCriteria(AuditTraceCriteria auditTraceCriteria)
        {

            IList<AuditTrace> ret = new List<AuditTrace>();

            foreach (AuditTrace audit in inMemoryStore.Values)
            {
                if (!String.IsNullOrEmpty(auditTraceCriteria.Category) && auditTraceCriteria.Category.Equals(audit.Category))
                {
                    ret.Add(audit);
                    continue;
                }

                if (!String.IsNullOrEmpty(auditTraceCriteria.User) && auditTraceCriteria.User.Equals(audit.User))
                {
                    ret.Add(audit);
                    continue;
                }

                if (audit.BusinessDate != null && auditTraceCriteria.StartBusinessDate != null && auditTraceCriteria.StartBusinessDate < audit.BusinessDate)
                {
                    if (auditTraceCriteria.EndBusinessDate == null)
                    {
                        ret.Add(audit);
                        continue;
                    }
                    else if (auditTraceCriteria.EndBusinessDate > audit.BusinessDate)
                    {
                        ret.Add(audit);
                        continue;
                    }
                }

                if (audit.ExecutionDate != null && auditTraceCriteria.StartExecutionDate != null && auditTraceCriteria.StartExecutionDate < audit.ExecutionDate)
                {
                    if (auditTraceCriteria.EndExecutionDate == null)
                    {
                        ret.Add(audit);
                        continue;
                    }
                    else if (auditTraceCriteria.EndExecutionDate > audit.ExecutionDate)
                    {
                        ret.Add(audit);
                        continue;
                    }
                }

                if (auditTraceCriteria.Item != null && auditTraceCriteria.Item == audit.Item)
                {
                    ret.Add(audit);
                }

            }

            return ret;
        }

    }
}
