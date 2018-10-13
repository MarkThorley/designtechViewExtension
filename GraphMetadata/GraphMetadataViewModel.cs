using Dynamo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;
using Dynamo.ViewModels;
using System.Windows;
using Dynamo.Graph;
using Dynamo.Wpf.Extensions;
using Dynamo.Graph.Annotations;
using Dynamo.Graph.Workspaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace designtechViewExtension
{
    class GraphMetadataViewModel : NotificationObject
    {
        #region Fields

        private ViewLoadedParams readyParams;
        private string office { get; set; }

        #endregion

        #region Property

        public ViewLoadedParams ReadyParams
        {
            get { return readyParams; }
            set { readyParams = value; }
        }

        public string Office
        {
            get { return office; }
            set { office = SetOfficeFromJSON(); }
        }


        #endregion

        #region Functions

        public string GetDictionaryValues()
        {
            string descString = readyParams.CurrentWorkspaceModel.Description;
            return descString;
        }

        public Dictionary<string, string> CreateDictionaryFromString(string str)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            char[] c1 = ",".ToCharArray();
            string[] firstSplit = str.Split(c1);

            char[] c2 = ":".ToCharArray();
            string[] secondSplit = str.Split(c2);

            return dict;
        }

        private string SetOfficeFromJSON()
        {
            string s = GetDictionaryValues();
            Dictionary<string, string> dict = CreateDictionaryFromString(s);
            string officeValue = dict["Office"];
            return officeValue;
        }




        #endregion

        #region ReadyParams
        public GraphMetadataViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
        }
        #endregion

        #region Dispose Methods
        public void Dispose()
        {
        }
        #endregion
    }
}
