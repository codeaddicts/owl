[![Build Status](https://travis-ci.org/codeaddicts/owl.svg?branch=master)](https://travis-ci.org/codeaddicts/owl)

What is owl?
----

owl is a html preprocessor which allows you to write websites using a syntax which is a bit like C.

This is why owl is _probably_ better than html:

* Supports escape characters like \t and \n for easier formatting
* Doesn't need ugly closing tags
* Generates flawless HTML code in no-time

Yeah, we know: That's not that much.    
That's why we are working hard to integrate more awesome features into the language!

How does owl look?
----

The following is an example of how owl markup looks like and what html code it generates.    
Input (owl markup):
```c#
head {
   title { "Hello!" }
   link ( rel = "stylesheet" href = "example.css" );
}

document {
   h1 { Welcome to the owl example page! }
   p {
      "We hope you like it!\nVisit our GitHub page: "
      a ( href = "https://gihub.com/SplittyDev/ewm" ) { "owl on GitHub!" }
   }
}
```

Output (HTML):
```html
<!DOCTYPE html>

<html>
  <head>
    <title>Hello!</title>
    <link type="text/css" rel="stylesheet" href="example.css">
  </head>

  <body>
    <h1>Welcome to the owl example page!</h1>

    <p>
      We hope you like it!<br>
      Visit our GitHub page:
      <a href="https://gihub.com/SplittyDev/ewm">owl on GitHub!</a>
    </p>
  </body>
</html>
````

As you can see, owl markup is easy to write and produces correctly formatted HTML code!
