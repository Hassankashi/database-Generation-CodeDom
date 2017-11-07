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
using System.Linq.Expressions;

namespace MVCNHibernate
{
    public class GenerateMap
    {
        CodeCompileUnit targetUnit;

        CodeTypeDeclaration targetClass;

        //Class Name
        private const string outputFileName = "ProductMap.cs";

        public GenerateMap(string tableName)
        {
            targetUnit = new CodeCompileUnit();
           
            //Path
            CodeNamespace samples = new CodeNamespace("MVCNHibernate.Mapping");
           
            //Namespace
            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            samples.Imports.Add(new CodeNamespaceImport("System.Linq"));
            samples.Imports.Add(new CodeNamespaceImport("System.Web"));
            samples.Imports.Add(new CodeNamespaceImport("FluentNHibernate.Mapping"));
            samples.Imports.Add(new CodeNamespaceImport("MVCNHibernate.Entities"));

            targetClass = new CodeTypeDeclaration(tableName+"Map");
            targetClass.BaseTypes.Add(new CodeTypeReference { BaseType = "ClassMap`1[" + tableName + "]", Options = CodeTypeReferenceOptions.GenericTypeParameter });
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public ;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);

        }

        public void AddConstructor(string fld1, string fld2, string tbl)
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            CodeExpression newType = new CodeExpression();
            CodeSnippetExpression snippet = new CodeSnippetExpression();

            string hh = string.Format("Table(\"{0}\"", tbl);
            string lambda = @"Id(x => x." + fld1 + "); Map(x => x." + fld2 + ");" + hh+")";

            var lambdaExpression = new CodeSnippetExpression(lambda);
           
            constructor.Statements.Add(lambdaExpression);

            targetClass.Members.Add(constructor);
        }


        public void RelationalAddConstructor(string fld1, string fld2, string tbl, string parent)
        {
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            CodeExpression newType = new CodeExpression();
            CodeSnippetExpression snippet = new CodeSnippetExpression();

            string parenttbl = parent + tbl;
            string fk=parent+"id";
            string cc= string.Format("\"{0}\"", fk);
            string hh = string.Format("Table(\"{0}\"", tbl);
            string lambda = @"Id(x => x." + fld1 + "); Map(x => x." + fld2 + "); References(x => x." + parenttbl + ").Column(" + cc + "); " + hh + ")";

            var lambdaExpression = new CodeSnippetExpression(lambda);
            
            constructor.Statements.Add(lambdaExpression);

            targetClass.Members.Add(constructor);
        }

        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
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