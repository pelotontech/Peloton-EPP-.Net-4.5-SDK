using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PelotonEppSdk.Models
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class StatementsResponse : Response
    {
        /// <summary>
        /// The opening balance of the account
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// A collection of <see cref="statement_detail"/>s 
        /// </summary>
        public ICollection<StatementDetail> StatementDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ToDate { get; set; }

        internal StatementsResponse(statements_response sr) : base(sr)
        {
            OpeningBalance = sr.opening_balance;
            // ReSharper disable once ExceptionNotDocumented
            // ReSharper disable once SuspiciousTypeConversion.Global
            StatementDetails = sr.statement_details.Select(sd => (StatementDetail) sd).ToList();
            FromDate = sr.from_date_utc;
            ToDate = sr.to_date_utc;
        }
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class StatementDetail
    {
        public string LedgerType { get; set; }

        public decimal Amount { get; set; }

        public TransactionDescription TransactionDescription { get; set; }

        public StatementTransactionType TransactionType { get; set; }

        /// <summary>
        /// A list of references associated with the transaction
        /// </summary>
        public ICollection<Reference> References { get; set; }

        /// <summary>
        /// Date and time which the transaction was recorded
        /// </summary>
        public DateTime TransactionDatetime { get; set; }

        /// <summary>
        /// a nullable transaction_reference_code
        /// </summary>
        public string TransactionReferenceCode { get; set; }

        internal StatementDetail(statement_detail sd)
        {
            LedgerType = sd.ledger_type;
            Amount = sd.amount;
            TransactionDatetime = sd.transaction_datetime_utc;
            TransactionReferenceCode = sd.transaction_reference_code;
            TransactionDescription = (TransactionDescription)sd.transaction_description;
            TransactionType = (StatementTransactionType)sd.transaction_type;
            References = sd.references.Select(r=>(Reference) r).ToList();
        }
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class StatementTransactionType
    {
        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType friendly name
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType as shown in database
        public string Code { get; set; }
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class TransactionDescription
    {
        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType friendly name
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType as shown in database
        public string Code { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class statements_response : response
    {
        /// <summary>
        /// The opening balance of the account
        /// </summary>
        public decimal opening_balance { get; set; }

        /// <summary>
        /// A collection of <see cref="statement_detail"/>s 
        /// </summary>
        public ICollection<statement_detail> statement_details { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime from_date_utc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime to_date_utc { get; set; }

        public static explicit operator StatementsResponse(statements_response sr)
        {
            return new StatementsResponse(sr);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class statement_detail
    {
        public string ledger_type { get; set; }

        public decimal amount { get; set; }

        public transaction_description transaction_description { get; set; }

        public transaction_type transaction_type { get; set; }

        /// <summary>
        /// A list of references associated with the transaction
        /// </summary>
        public ICollection<reference> references { get; set; }

        /// <summary>
        /// Date and time which the transaction was recorded
        /// </summary>
        public DateTime transaction_datetime_utc { get; set; }

        /// <summary>
        /// a nullable transaction_reference_code
        /// </summary>
        public string transaction_reference_code { get; set; }

        public static explicit operator StatementDetail(statement_detail sd)
        {
            return new StatementDetail(sd);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class transaction_type
    {
        /// <summary>
        /// 
        /// </summary>
        // JournalType friendly name
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // JournalType as shown in db
        public string code { get; set; }

        public static explicit operator StatementTransactionType(transaction_type td)
        {
            return new StatementTransactionType
            {
                Code = td.code,
                Name = td.name
            };
        }
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    internal class transaction_description
    {
        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType friendly name
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // JournalTransactionType as shown in database
        public string code { get; set; }

        public static explicit operator TransactionDescription(transaction_description td)
        {
            return new TransactionDescription
            {
                Code = td.code,
                Name = td.name
            };
        }
    }
    
}