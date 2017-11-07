# database-Generation-CodeDom
It enables you to generate table in any database such as Oracle, SQLServer, MySQL, SQLite, Sybase and etc just by typing table name and its fields. Then you can use NHibernate facilities to make query for data layer.

This describes how to allow users to build database automatically from application via two technologies code generation (CodeDOM) and NHibernate (Fluent) which let us make backend regardless of type of database and without involving into complicated concepts.
First of all I want to mention why I have selected NHibernate or in a better words Fluent NHibernate to do this. I have counted some of most popular advantage and disadvantage of NHibernate.
NHibernate
NHibernate is an Object Relational Mapping ORM framework. NHibernate locates between database and business layer and provides powerful mapping.  It inherits from Hibernate from Java world. It uses mapping file or attributes besides properties.
NHibernate Advantages:
Powerful mapping facilities.
Execute multiple queries in one going to database instead of going to DB for each query.
Lazy Loading works for NHibernate and it means that you just fetch your necessary data on memory instead of the whole of collection in memory and it reduces overhead from memory.
Decoupling from database, it means you can use various database type instead of just using SQL such as Oracle and etc.
Writing code for NHibernate make developers feel better in the aspect of readability and documentary.
 Cashing and second level caching.
Its session factory is thread safe object in NHibernate.
 
NHibernate Disadvantages:
Learning and mastering in NHibernate takes time and it seems cumbersome to be professional.
NHibernate needs complicated XML configuration per entities.
 It takes long time when you initiate it for first time because of preparation operation in metadata is heavy.
How NHibernate works?
NHibernate provides an ORM as free XML configuration. It depends on session factory which creates session in order to bind database. You need a class to define your specific configuration which introduces connection string for particular database. After that whenever you call session you are able to connect to your db. You can either write a code as traditional sql query or Linq query by using NHibernate.Linq as your namespace. Totally session encapsulate unit of work pattern too. You need to produce XML files to work with NHibenate but I have used Fluent NHibernate to prevent any cumbersome during implementation.



CodeDOM 
By Fluent NHibernate you need to write two classes for per entity as your table, one is simple and another for mapping and introduce relation to another table. It is a bit difficult if you use so many tables in your project to write two classes for per entity. Therefore I decided to use Code Document Object Model, CodeDOM, as a generation tool for generating these classes and I get table name, field names and their data type for parent table from UI and for child ones I get its parent table in order to build foreign key.
As a simplest explanation about CodeDOM: please image you have to generate so many classes with same structures but some parts have different expression so in order to save your time and energy it is better to write one class instead of so many class and that class as a good template generates all of your classes.




By using CodeDOM you can either write source code for ASP.NET, XML web services client proxies and etc or compile. Its structure is such a tree graph which its root is codecompileunit. Then you should define your namespace and import them. Determine your class name and fields and properties and add them as members to class.
CodeDOM Advantages:
        1. CodeDOM let you create, compile and execute your source code for application at run time, without                      writing a lot of lines or when your parameter should be determined at run time.
        2. CodeDOM uses single model to generate source code, so any languages that support CodeDOM can work             with it.
        3. It is possible to translate VB to C# or inverse.
CodeDOM Disadvantages:
        1. There are limitation to define any expression by CodeDOM which you have to use Snippet classes.
        2. Snippet classes can not satisfy any expression so you should not use heavy function on that.
 
Using the code Step by Step
1. Creating Database 
You should create database as "GenerateDB" which is a fundamental requirement for connection string. I have used SQL Server 2008 but you can use any database because NHibernate is over from database type.



I strongly recommend you to read this article:
https://www.codeproject.com/Articles/891056/Automatic-Table-Generation-in-any-database-by-NHib


