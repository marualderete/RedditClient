# Obective
Create a simple Reddit client that shows the top 50 entries from Reddit

# Show your work
Create a Public repository
Commit each step of your process so we can follow your thought process.

# Guidelines
To do this please follow these guidelines:

- Assume the latest platform and use Xamarin Forms
- Support all Device Orientation
- Support all Devices screen sizes

# What to show
The app should be able to show data from each entry such as:
- Title (at its full length, so take this into account when sizing your cells)
- Author
- entry date, following a format like “x hours ago” 
- A thumbnail for those who have a picture.
- Number of comments
- Unread status
In addition, for those having a picture (besides the thumbnail), please allow the user to tap on the thumbnail to be sent to the full sized picture. You don’t have to implement the IMGUR API, so just opening the URL would be OK.

# What to Include
- Pull to Refresh
- Pagination support
- Saving pictures in the picture gallery
- App state-preservation/restoration
- Indicator of unread/read post (updated status, after post it’s selected)
- Dismiss Post Button (remove the cell from list. Animations required)
- Dismiss All Button (remove all posts. Animations required)
- Support split layout (left side: all posts / right side: detail post)

# Resources
- [Reddit API](http://www.reddit.com/dev/api)
- [Apigee](https://apigee.com/console/reddit)
- Example JSON file (top.json) is listed.
- Example Video of functionality is attached

# RedditClient
A Xamarin Forms app that consumes Reddit Appi. It shows show user posts on a master detail, as a menu.
And take you to post details when tapped.
In pos detail page You can download the image or go to the external site.

# SOLUTION RESPONSE:
#In this project I implement:

* Pull to refresh
* Pagination Support in post list
* Saving images to an external storage (mobile gallery) implementing DependencyService on each platform
* Reddit API implementation in order to dismiss a post (and remove item from the list) or dismiss all the post that are shown
* Split layout support with master detail page.
* CustomView pages
* Mvvm pattern
* A ServiceLocator class in order to instanciate services
* StopWatch implementation in order to refresh api access_token every hour


# TO DO List

* Because of time I didn't make an indicator of unread/read post (updated status, after post it’s selected)


