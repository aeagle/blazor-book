# blazor-book

A Storybook like UI for hosting Blazor components

<img src="https://www.allaneagle.com/blazorbook.gif?v=1" width="100%" />

## Installation

The `BlazorBook` nuget package needs to be added to your Blazor WASM/Server app and your Razor Class library project containing your individual stories.

`Install-Package BlazorBook -ProjectName MyProject`

## Setup

In the startup of your Blazor WASM/Server app:

`app.RegisterAllStories(Assembly.Load("<<Assembly name containing stories>>"));`

- In `_Imports.razor`

Add `@using BlazorBook.Components`

- In `App.razor`

  1. Add <BlazorBook.Resources /> to the top
  2. Add an AdditionalAssemblies prop on the <Router /> component as follows:

```html
<Router
  AppAssembly="@typeof(App).Assembly"
  AdditionalAssemblies="new[] { typeof(BlazorBook.UI).Assembly }"
>
  ...
</Router>
```

The stories will be registered and the UI available on the path `/blazorbook`

## Stories

Create a Razor Class Library to contain your stories. This should also have the `BlazorBook` nuget package as a dependency.

For example a story can be as follows:

### HelloWorld.razor

```
@inherits BlazorBook.StoryComponent
@attribute [DisplayName("Story name")]

<div>Hello @(world)!</div>

@code {
    string world = "World!";
}
```
