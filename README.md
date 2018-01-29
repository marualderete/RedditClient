# RedditClient
A Xamarin Forms app that consumes Reddit Appi. It shows show user posts on a master detail, as a menu.
And take you to post details when tapped.
In pos detail page You can download the image or go to the external site.


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


