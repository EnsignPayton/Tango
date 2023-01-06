# Tango

Nothing to see here.

## MVP

* Collect Anki card data, display number of words known
* Word -> Notes/Cards -> Intervals
* Probably only look at the longest interval non-suspended card.
* Interpret interval into some "knowledge factor" percentage. Possibly offer some options, such as a time scaling factor (how long equals 100% known), time bias factor (should this be linear, logarithmic, etc), do we take the best card or average them, etc.
* Display known words and allow to filter by knowledge factor - ex. how many words are 100% known? 50%? At least 1%?
* Easiest mvp is a standalone console app that pulls data over anki connect and has all options hard coded.

## Extended Ideas

* Scrape data from Anki and store in a separate database
* Create web-based UI for displaying collected data
* Allow browsing individual words, showing their knowledge factor, what data sources it was pulled from, and details of those data sources
* Also integrate the WaniKani API to allow integrating those words without actually making Anki cards for them
* Integrate with JPDB for readability analysis
* Parse sentence cards to analyze their readability, but don't actually include them in the known words list. This would require a second tier of data source, listing it as "referenced" but not contributing to the knowledge factor
* Very optional and late game feature - i+1 card reordering.

Do all this and I can delete Morphman
