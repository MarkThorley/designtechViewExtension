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
using System.Reflection;

namespace designtechViewExtension
{
    public class GraphMetadataViewModel : NotificationObject
    {
        #region Fields

        private ViewLoadedParams readyParams;
        private Dictionary<string, string> dict;
        #endregion

        #region Property

        public ViewLoadedParams ReadyParams
        {
            get { return readyParams; }
            set { readyParams = value; }
        }

        public string Office
        {
            get
            {
                return dict["Office"];
            }
            set
            {
                dict["Office"] = value;
                this.RaisePropertyChanged("Office");
            }
        }

        public string Author
        {
            get
            {
                return dict["Author"];
            }
            set
            {
                dict["Author"] = value;
                this.RaisePropertyChanged("Author");
            }
        }

        public string Description
        {
            get
            {
                return dict["Description"];
            }
            set
            {
                dict["Description"] = value;
                this.RaisePropertyChanged("Description");
            }
        }

        public string Category
        {
            get
            {
                return dict["Category"];
            }
            set
            {
                dict["Category"] = value;
                this.RaisePropertyChanged("Category");
            }
        }

        public string Tags
        {
            get
            {
                return dict["Tags"];
            }
            set
            {
                dict["Tags"] = value;
                this.RaisePropertyChanged("Tags");
            }
        }

        public string ScriptVersion
        {
            get
            {
                return dict["ScriptVersion"];
            }
            set
            {
                dict["ScriptVersion"] = value;
                this.RaisePropertyChanged("ScriptVersion");
            }
        }

        public string DynamoVersion
        {
            get
            {
                return dict["DynamoVersion"];
            }
            set
            {
                dict["DynamoVersion"] = value;
                this.RaisePropertyChanged("DynamoVersion");
            }
        }

        public string LastTested
        {
            get
            {
                return dict["LastTested"];
            }
            set
            {
                dict["LastTested"] = value;
                this.RaisePropertyChanged("LastTested");
            }
        }
        #endregion


        #region Functions

        public string GetScriptDescription()
        {
            return readyParams.CurrentWorkspaceModel.Description;
        }

        public void SetScriptDescription()
        {
            string desc = CreateStringFromDictionary(dict);

            //Reflection Method to set read-only property description
            WorkspaceModel wm = readyParams.CurrentWorkspaceModel as WorkspaceModel;
            PropertyInfo nameProperty = wm.GetType().GetProperty("Description");
            nameProperty.SetValue(wm, desc);
        }

        public Dictionary<string, string> CreateDictionaryFromString(string str)
        {
            dict = new Dictionary<string, string>();
            if (str == null)
            {
                dict.Add("Office", "");
                dict.Add("Author", "");
                dict.Add("Description", "");
                dict.Add("Category", "");
                dict.Add("Tags", "");
                dict.Add("ScriptVersion", "");
                dict.Add("DynamoVersion", "");
                dict.Add("LastTested", "");
            }
            else
            {
                char c1 = '|';
                string[] firstSplit = str.Split(c1);
                foreach (string s in firstSplit)
                {
                    char c2 = ':';
                    string[] dictItem = s.Split(c2);
                    dict.Add(dictItem[0], dictItem[1]);
                }
            }
            return dict;
        }

        public string CreateStringFromDictionary(Dictionary<string, string> dict)
        {
            return string.Join("|", dict.Select(x => x.Key + ":" + x.Value).ToArray());
        }

        #endregion


        #region ReadyParams
        public GraphMetadataViewModel(ReadyParams p)
        {
            readyParams = p as ViewLoadedParams;
            dict = CreateDictionaryFromString(p.CurrentWorkspaceModel.Description);
        }
        #endregion




        #region Dispose Methods
        public void Dispose()
        {
        }
        #endregion
    }
}
