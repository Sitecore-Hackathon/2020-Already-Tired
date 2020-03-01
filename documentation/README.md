
# Documentation

## Summary

**Category:** Sitecore Meetup Website

My Hackathon submission was built with the intention of being a place where Sitecore User Groups can manage their groups and add scheduled events and meetings to the groups. Users are also able to sign up for an account and indicate that they are going to attend posted events.

## Pre-requisites
- Sitecore JSS
- Hasura ([https://hasura.io/](https://hasura.io/))- I used this to store user generated data. I have a publicly hosted Hasura instance I can provide the API key for testing. Just didn't want to commit that key to source control. Message me!

## Installation

There are two parts to the installation: a GraphQL extension library and the Sitecore JSS application.

#### GraphQL extension library
* Navigate to `src\ScMeetupGraphQLExtensions`, open `ScMeetupGraphQLExtensions.sln`. Built with Visual Studio 2019 and targeted .NET 4.7.2.
* Build the solution. References are all NuGet packages so they should restore automatically from the Sitecore NuGet Repository.
* Open the `src\ScMeetupGraphQLExtensions\ScMeetupGraphQLExtensions\bin\Debug` folder, copy `ScMeetupGraphQLExtensions.dll` to your Sitecore `bin`.

#### Sitecore JSS application
* Ensure you have Sitecore JSS (server and CLI) installed and your API key already created.
* Navigate to `src\scmeetup`, type `npm install` in a command line.
* Type `jss setup`, follow the steps. Add a binding for testing this locally, I was using `http://scmeetup.dev.local`. Add a host entry for this. 
* Run `jss deploy config` after the CLI steps have completed.
* Run `jss build`
* Run `jss deploy app -c -d`
* Run `jss start:connected`
* Open your browser to `http://localhost:3000`
* You should be presented with the homepage.

## Configuration

The only configuration that needs to be done is for Hasura, where user generated data is being stored. I can/will provide these two values for testing.

```xml
<?xml version="1.0"?>
<!--
  Purpose: Configuration settings for my hackathon module
-->
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <settings>
      <setting name="ScMeetup.HasuraSecret" value="SECRET_HERE" />
      <setting name="ScMeetup.HasuraEndpoint" value="ENDPOINT_HERE" />
    </settings>
  </sitecore>
</configuration>
```

## Usage

My module provides a place for people to browse, post and join events and groups.

![Homepage](images/scmeetup-home.png?raw=true "Homepage")

Users are able to browse all upcoming events listed in the system:
![Upcoming Events](images/scmeetup-upcomingevents.png?raw=true "Upcoming Events")

If you already have an account, you can login with your name and password. This feature leverages Sitecore Authentication and is performed via a GraphQL mutation.
![Login](images/scmeetup-login.png?raw=true "Login")

If you do not have an account already, you can sign up for one using the Sign Up page:
![Sign Up](images/scmeetup-signup.png?raw=true "Sign Up")

Meetings can be browsed and you can indicate your interest in attending by clicking the "Attend" button in the side navigation. The attend button will indicate your joined status and gives you the opportunity to unjoin.
![Attend](images/scmeetup-attend.png?raw=true "Attend")

You can view existing groups that are already created and navigate to their detail pages. You're also able to create your own using the sidebar call out button.
![Group Listing](images/scmeetup-grouplist.png?raw=true "Group Listing")

You can view the group detail and choose to join this group and are able to create events that belong to this group. The button status changes depending on whether or not you have already joined.
![Group Detail](images/scmeetup-groupdetail.png?raw=true "Group Detail")

## Video

I've recorded a small walk through of the application and have added it to YouTube [here](https://www.youtube.com/watch?v=tWYaVcPBr8g).
