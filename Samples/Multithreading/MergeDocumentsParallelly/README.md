# Merge PDF documents in parallel threads
This sample shows how to parallelize merging of PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The code splits input documents to groups of the `rangeSize` size. Then, each group is merged into
intermediate documents in parallel. The process is continued until the number of input documents
is small enough for the simple merging.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Merge two PDF documents](/Samples/General%20operations/MergeDocuments) code sample
