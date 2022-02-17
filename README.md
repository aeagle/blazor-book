# blazor-book

A Storybook like UI for hosting Blazor components

## Installation

`Install-Package Elmah -ProjectName MyProject`

## Setup

In the startup of your Blazor WASM/Server app:

`app.RegisterAllStories(Assembly.Load("<<Assembly name containing stories>>"));`

The stories will be registered and the UI available on the path `/blazorbook`

## Stories

Create a Razor Class Library to contain your stories.

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