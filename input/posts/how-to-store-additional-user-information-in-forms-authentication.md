Title: How to Store Additional User Information in Forms Authentication
Published: 2018-12-07
Category: professional
---
When using Forms Authentication, it can be desirable to store additional information with the authentication cookie, like the user's first name, or whether or not they are an admin. Thankfully, while not super intuitive, Forms Authentication does provide this capability, using the FormsAuthenticationTicket's UserData property.

The UserData property is a `string`, so you can store pipe-separated data, or even a serialized object.

### The Code

<script src="https://gist.github.com/dochoffiday/badfd41c4b1bc51971d00080d157943b.js"></script>

To see the full gist, [click here](https://gist.github.com/dochoffiday/badfd41c4b1bc51971d00080d157943b).


### Further Reading

* [Forms Authentication Configuration and Advanced Topics (C#)](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/older-versions-security/introduction/forms-authentication-configuration-and-advanced-topics-cs#step-4-storing-additional-user-data-in-the-ticket)