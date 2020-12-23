Title: "The Type or Namespace 'Script' Doe snot Exist in the Namespace 'SYSTEM.WEB' (Are You Missing An Assembly Reference?)"
Published: 2012-05-03
Category: professional
---
Sound familiar? How about one of these:

1. The type or namespace name 'KeyValueConfigurationCollection' could not be found (are you missing a using directive or an assembly reference?)
2. The type or namespace name 'Serialization' does not exist in the namespace 'System.Xml' (are you missing an assembly reference?)
3. The type or namespace name 'SqlClient' does not exist in the namespace 'System.Data' (are you missing an assembly reference?)

The underlying problem is that the assembly being referenced isn’t included in the GAC (global assembly cache) on the server. Since developers typically have more control over their local environments, this error is often only exposed when a project is deployed to a production server.

### The "Web Project" Solution

If, in Visual Studio, the project is a Web Project (as opposed to a Web Site), the solution is relatively easy. In your references folder, right-click the assembly in question and click “Properties.” From the properties window, toggle the “Copy Local” attribute to “true.”

This ensures that the .dll for the assembly gets deployed along with your project, as [outlined here](http://stackoverflow.com/questions/10341526/is-there-an-explanation-behind-the-error-the-type-or-namespace-name-script-do#10341610).

### The "Web Site" Solution

What if, however, your project is a Web Site? In a Web Site, this list of references is not available, removing the “Copy Local” fix as an option.

Luckily, all is not lost. All we have to do is manually add the reference into the web.config file.

This is easy if we know which assembly we have to add; however, not only do we not always know the assembly, but there’s no easy way to get the “PublicTokenNumber” either.

### Determining The Namespace

Going back to the “Script” example, the error reveals some other useful information, mainly the erroneous file and line number.

> Exception message: c:\[file information]\Extensions.cs(10): error CS0234: The type or namespace name 'Script' does not exist in the namespace 'System.Web' (are you missing an assembly reference?)

```
using System.Web.Script.Serialization;
```

So we now know that we’re trying to use the “System.Web.Script.Serialization” namespace, but we don’t know yet which assembly this is included in.

### Determining The Assembly

The quick-and-dirty way to determine this is to pull out the “using” line, see what class throws an error, then use reflection to get the assembly name for that class.

In my example, when I remove the “using” line I get the following error:

> Error 1 The type or namespace name 'JavaScriptSerializer' could not be found (are you missing a using directive or an assembly reference?)

on this line:

```
JavaScriptSerializer jss = new JavaScriptSerializer();
```

So, this is now what we know: the "JavaScriptSerializer" is under the "System.Web.Script.Serialization" namespace. But we still don’t know the assembly.

To grab the assembly, use the following piece of reflection to not only get the assembly, but the exact line needed to add to the web.config to include that assembly manually in the project.

```
System.Diagnostics.Debug.WriteLine(String.Format("<add assembly=\"{0}\" />", System.Reflection.Assembly.GetAssembly(typeof(JavaScriptSerializer)).FullName));
```

(if you're problem stems from a different class, just swap it out with the "JavaScriptSerializer" class)

When executing that line, we get the following result:

>  <add assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />

(reference [this page](http://stackoverflow.com/questions/2644908/visual-c-sharp-2010-express-output-window) if you are using Visual Studio Express and are unsure of how to view the “Output” window)

### Modifying The Web.Config

Now that we have the information we need, all we have to do is add it to the web.config.

```
<configuration>
    <system.web>
    <compilation debug="true" targetFramework="4.0">
        <assemblies>
        <add assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
        </assemblies>
    </compilation>
    </system.web>
</configuration>
```

And with that, we’re safe to deploy!