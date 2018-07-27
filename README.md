# Chance To Beat

A LiveSplit component to estimate the probability of beating a specified time.

## Install

Download the lastest version from the [Releases page](../../releases) and put
LiveSplit.ChanceToBeat.dll in LiveSplit's components folder.

## Setup

Add the component to the Layout (Information -> Chance To Beat). From here you
can change the text displayed (so like "PB Chance" or "Sub 1 Chance") and the
target time. For the target time, you can either fix to your Personal Best or
to a custom time.

The weight is how much to take older attempts into consideration. The extremes
are 0 (only look at the last splits for each segments) and 1 (treat all attempts
equally). Lower is better for quick improvement, higher is better for slow
improvement. For comparison, the average segments feature that comes with
LiveSplit uses 0.75.

You can also set the probability for the run ending on any given segment. This
is particularly good if the run can die at a certain point due to RNG, or it's
easy to take a death, and that isn't reflected in the splits.