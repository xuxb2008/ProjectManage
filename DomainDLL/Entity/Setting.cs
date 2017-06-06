using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// Setting object for NHibernate mapped table 'Setting'.
    /// </summary>
    public class Setting : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        public virtual string WeeklySend
        {
            get;
            set;
        }
        public virtual string WeeklyCC
        {
            get;
            set;
        }
        public virtual string WeeklyTitle
        {
            get;
            set;
        }
        public virtual string WeeklyContent
        {
            get;
            set;
        }
        public virtual string WeeklyCheck
        {
            get;
            set;
        }
        public virtual int? PubEmailType
        {
            get;
            set;
        }
        public virtual string PubEmail
        {
            get;
            set;
        }
        public virtual string PubSelfInfo
        {
            get;
            set;
        }
        public virtual string PubQQ
        {
            get;
            set;
        }
        public virtual string PubWeChat
        {
            get;
            set;
        }
        public virtual string PubTel
        {
            get;
            set;
        }
        public virtual int? WarnCost
        {
            get;
            set;
        }
        public virtual int? WarnPubDay
        {
            get;
            set;
        }
        public virtual int? WarnUpdateDay
        {
            get;
            set;
        }
        public virtual int? WarnOver
        {
            get;
            set;
        }
        public virtual int? WarnNearing
        {
            get;
            set;
        }
        public virtual int? WarnNoDeal
        {
            get;
            set;
        }

    }
}