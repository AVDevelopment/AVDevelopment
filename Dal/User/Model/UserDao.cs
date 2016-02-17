using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.User.Model
{

    /// <summary>
    /// UserDao object for table 'UM_User'.
    /// </summary>
    public partial class UserDao : BaseDao, ICloneable
    {

        #region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual string FirstName
        {
            get;
            set;

        }

        public virtual string LastName
        {
            get;
            set;

        }

        public virtual string UserName
        {
            get;
            set;

        }

        public virtual byte[] Password
        {
            get;
            set;

        }


        public virtual string SaltPassword
        {
            get;
            set;

        }

        public virtual string Email
        {
            get;
            set;

        }

        public virtual string Image
        {
            get;
            set;
        }

        public virtual int WebId
        {
            get;
            set;

        }


        public virtual DateTime RegistrationDate
        {
            get;
            set;

        }

        public virtual DateTime LastLogin
        {
            get;
            set;

        }

        public virtual bool Gender
        {
            get;
            set;

        }


        public virtual bool password_reset
        {
            get;
            set;

        }

        public virtual bool IsActive
        {
            get;
            set;

        }

        #endregion

        #region ICloneable methods

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion


    }

}
