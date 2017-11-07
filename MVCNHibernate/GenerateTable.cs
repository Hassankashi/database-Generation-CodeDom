using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;


namespace MVCNHibernate
{
    public class GenerateTable
    {
        CodeCompileUnit targetUnit;

        CodeTypeDeclaration targetClass;

        //Class Name
        private const string outputFileName = "Product.cs";

        public GenerateTable(string tableName)
        {
            targetUnit = new CodeCompileUnit();

            //Path
            CodeNamespace samples = new CodeNamespace("MVCNHibernate.Entities");
            
            //Namespace
            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            samples.Imports.Add(new CodeNamespaceImport("System.Linq"));
            samples.Imports.Add(new CodeNamespaceImport("System.Text"));
            samples.Imports.Add(new CodeNamespaceImport("MVCNHibernate.Entities"));

            targetClass = new CodeTypeDeclaration(tableName);
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);
        }

        public void AddFields(string fld1, string dt1, string fld2, string dt2)
        {
            CodeMemberField field1 = new CodeMemberField();
            field1.Attributes = MemberAttributes.Private;
            if (dt1=="int")
            {
                field1.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt1 == "string")
            {
                field1.Type = new CodeTypeReference(typeof(System.String));
            }
            
            field1.Name = "_"+fld1;
            targetClass.Members.Add(field1);

            CodeMemberProperty property1 = new CodeMemberProperty();
            property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld1)));
            property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld1), new CodePropertySetValueReferenceExpression()));
            property1.Attributes = MemberAttributes.Public ;
            property1.Name = fld1;
            if (dt1 == "int")
            {
                property1.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt1 == "string")
            {
                property1.Type = new CodeTypeReference(typeof(System.String));
            }
           
            targetClass.Members.Add(property1);

            CodeMemberField field2 = new CodeMemberField();
            field2.Attributes = MemberAttributes.Private;
            if (dt2 == "int")
            {
                field2.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt2 == "string")
            {
                field2.Type = new CodeTypeReference(typeof(System.String));
            }
            field2.Name = "_" + fld2;
            targetClass.Members.Add(field2);

            CodeMemberProperty property2 = new CodeMemberProperty();
            property2.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld2)));
            property2.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld2), new CodePropertySetValueReferenceExpression()));
            property2.Attributes = MemberAttributes.Public ;
            property2.Name = fld2;
            if (dt2 == "int")
            {
                property2.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt2 == "string")
            {
                property2.Type = new CodeTypeReference(typeof(System.String));
            }
           
            targetClass.Members.Add(property2);
        }
        
        public void RelationalAddFields(string tableName,string fld1, string dt1, string fld2, string dt2, string parent)
        {
            CodeMemberField field1 = new CodeMemberField();
            field1.Attributes = MemberAttributes.Private;
            if (dt1 == "int")
            {
                field1.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt1 == "string")
            {
                field1.Type = new CodeTypeReference(typeof(System.String));
            }

            field1.Name = "_" + fld1;
            targetClass.Members.Add(field1);

            CodeMemberProperty property1 = new CodeMemberProperty();
            property1.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld1)));
            property1.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld1), new CodePropertySetValueReferenceExpression()));
            property1.Attributes = MemberAttributes.Public;
            property1.Name = fld1;
            if (dt1 == "int")
            {
                property1.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt1 == "string")
            {
                property1.Type = new CodeTypeReference(typeof(System.String));
            }

            targetClass.Members.Add(property1);

            CodeMemberField field2 = new CodeMemberField();
            field2.Attributes = MemberAttributes.Private;
            if (dt2 == "int")
            {
                field2.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt2 == "string")
            {
                field2.Type = new CodeTypeReference(typeof(System.String));
            }
            field2.Name = "_" + fld2;
            targetClass.Members.Add(field2);

            CodeMemberProperty property2 = new CodeMemberProperty();
            property2.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld2)));
            property2.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld2), new CodePropertySetValueReferenceExpression()));
            property2.Attributes = MemberAttributes.Public;
            property2.Name = fld2;
            if (dt2 == "int")
            {
                property2.Type = new CodeTypeReference(typeof(System.Int32));
            }
            else if (dt2 == "string")
            {
                property2.Type = new CodeTypeReference(typeof(System.String));
            }

            targetClass.Members.Add(property2);

            CodeMemberField field3 = new CodeMemberField();
            field3.Attributes = MemberAttributes.Private;
            
            // field3.Type = new CodeTypeReference(typeof(System.Int32));
            Type myType = Type.GetType("MVCNHibernate.Entities."+parent);
            //dynamic instance = Activator.CreateInstance(myType);

            field3.Type = new CodeTypeReference(myType);

            field3.Name = "_" + parent+tableName;
            targetClass.Members.Add(field3);

            CodeMemberProperty property3 = new CodeMemberProperty();
            property3.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + parent + tableName)));
            property3.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + parent + tableName), new CodePropertySetValueReferenceExpression()));
            property3.Attributes = MemberAttributes.Public;
            property3.Name = parent + tableName;
            Type myType2 = Type.GetType("MVCNHibernate.Entities." + parent);
           // dynamic instance2 = Activator.CreateInstance(myType2);
            property3.Type = new CodeTypeReference(myType2);

            targetClass.Members.Add(property3);
        }
   
        CodeDomProvider provider;
        public void GenerateCSharpCode(string fileName)
        {
             
             provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    targetUnit, sourceWriter, options);
            }
        }
    }
}