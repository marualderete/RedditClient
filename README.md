## RedditClient
Author: María Inés Alderete 
Year: 2018

## Description
A Xamarin Forms app that consumes Reddit API. It shows show user posts on a master detail (as a menu) and take you to post details when you tap one item from the post list.

In post detail page, you can download the image (if the post has one) or go to the external site.


## In this project I implement:

* Pull to refresh
* Pagination Support in post list
* Saving images to an external storage (mobile gallery) implementing DependencyService on each platform
* Reddit API implementation in order to dismiss a post (and remove item from the list) or dismiss all the post that are shown
* Split layout support with master detail page.
* CustomView pages
* Mvvm pattern (xamarin forms)
* A ServiceLocator class in order to instanciate services
* StopWatch implementation in order to refresh api access_token every hour

## Technologies used in this project:
- Xamarin Forms
- Newtonsoft.Json
- ImageCircle.Forms.Plugin

## TO DO List

* Because of time I didn't make an indicator of unread/read post (updated status, after post it’s selected)
* I will implement a custom task scheduller in order to improve async tasks.

## Need help with something?
Text me at maru.ferri@gmail.com, and I see what can I do for you :)
