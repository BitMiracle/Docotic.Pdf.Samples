# Merge PDF documents in parallel threads

This sample shows how to merge PDF documents in parallel using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The code divides the input documents into groups of `rangeSize` documents. Each group is then merged into an intermediate PDF document, with multiple groups processed in parallel. This process repeats until the number of remaining documents is small enough to merge sequentially.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Merge two PDF documents](/Samples/General%20operations/MergeDocuments) code sample
