using System;
using System.Collections;
using System.Collections.Generic;
using AV.Development.Core.User.Interface;
using System.Security.Cryptography;

namespace AV.Development.Core.User
{

    /// <summary>
    /// User object for table 'UM_User'.
    /// </summary>
    public class User : IUser, ICloneable
    {
        #region Member Variables

        protected int _id;
        protected string _firstname;
        protected string _lastname;
        protected string _username;
        protected byte[] _password;
        protected string _saltPassword;
        protected string _email;
        protected string _image;
        protected string _OldPassword;
        protected bool _gender;
        protected int _webid;
        protected int _currentwebid;
        protected int _usertype;
        protected bool _isactive;
        protected bool _ispwdreset;
        protected DateTime _lastlogin;
        protected DateTime _registrationDate;

        #endregion

        #region Constructors
        public User() { }

        public User(string pFirstName, string pLastName, string pUserName, byte[] pPassword, string pEmail, string pImage, string psaltPassword, string pOldPassword, int pWebId, bool pIsactive, bool pIsPwdreset, DateTime pRegistrationDate, DateTime pLastLogin, int pCurrentwebID, int pUserType)
        {
            this._firstname = pFirstName;
            this._lastname = pLastName;
            this._username = pUserName;
            this._password = pPassword;
            this._email = pEmail;
            this._image = pImage;
            this._saltPassword = psaltPassword;
            this._OldPassword = pOldPassword;
            this._webid = pWebId;
            this._currentwebid = pCurrentwebID;
            this._usertype = pUserType;
            this._isactive = pIsactive;
            this._ispwdreset = pIsPwdreset;
            this._registrationDate = pRegistrationDate;
            this._lastlogin = pLastLogin;
            this._ispwdreset = pIsPwdreset;
        }

        public User(int pId)
        {
            this._id = pId;
        }

        #endregion

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }

        }

        public string FirstName
        {
            get { return _firstname; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("FirstName", "FirstName value, cannot contain more than 50 characters");
                _firstname = value;
            }

        }

        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("LastName", "LastName value, cannot contain more than 50 characters");
                _lastname = value;
            }

        }

        public string UserName
        {
            get { return _username; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("UserName", "UserName value, cannot contain more than 250 characters");
                _username = value;
            }

        }

        public byte[] Password
        {
            get { return _password; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Password", "Password value, cannot contain more than 50 characters");
                _password = value;
            }

        }

        public string SaltPassword
        {
            get { return _saltPassword; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Password", "Password value, cannot contain more than 50 characters");

                _saltPassword = value;
            }

        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Email", "Email value, cannot contain more than 250 characters");
                _email = value;
            }

        }

        public string Image
        {
            get { return _image; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Image", "Image value, cannot contain more than 250 characters");
                _image = value;
            }

        }


        public bool Gender
        {
            get { return _gender; }
            set { _gender = value; }

        }


        public virtual string OldPassword
        {
            get { return _OldPassword; }
            set { _OldPassword = value; }

        }

        public virtual int WebId
        {
            get { return _webid; }
            set { _webid = value; }

        }

        public virtual int Currentwebid
        {
            get { return _currentwebid; }
            set { _currentwebid = value; }

        }
        public virtual int UserType
        {
            get { return _usertype; }
            set { _usertype = value; }

        }

        public virtual DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = value; }

        }

        public virtual DateTime LastLogin
        {
            get { return _lastlogin; }
            set { _lastlogin = value; }

        }
        public virtual bool IsPasswordReset
        {
            get { return _ispwdreset; }
            set { _ispwdreset = value; }

        }

        public virtual bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }

        }


        #endregion

        #region Equals And HashCode Overrides
        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            User castObj = null;
            try
            {
                castObj = (User)obj;
            }
            catch (Exception) { return false; }
            return (castObj != null) &&
                (this._id == castObj.Id);
        }
        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {


            int hash = 57;
            hash = 27 * hash * _id.GetHashCode();
            return hash;
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
