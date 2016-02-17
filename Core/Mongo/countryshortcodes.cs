using System;
using Norm;
using Norm.Attributes;
using AV.Development.Dal.Mongo.Context;

namespace AV.Development.Core.Mongo
{
    public class countryshortcodes
    {

        public ObjectId Id { get; private set; }
        public string name { get; set; }
        public string code { get; set; }
       
    }
}