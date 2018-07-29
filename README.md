# Chance To Beat

A LiveSplit component to estimate the probability of beating a specified time.
Inspired by SethBling's PBChance component.

## Install

Download the latest version from the [Releases page](../../releases) and put
LiveSplit.ChanceToBeat.dll in LiveSplit's components folder.

## Setup

Add the component to the Layout (Information -> Chance To Beat). From here you
can change the text displayed (e.g. "PB Chance" or "Sub 1 Chance") and the
target time. For the target time, you can either lock to your Personal Best or
use a custom time.

The weight is how much to take older attempts into consideration. The extremes
are 0 (only look at the last splits for each segments) and 1 (treat all attempts
equally). Lower is better for quick improvement, higher is better for slow
improvement. For comparison, the average segments feature that comes with
LiveSplit uses a weight of 0.75. The weight is a multiple of 0.001, and if you
want to be precise you can change the slider by very small amounts with the left
and right arrow keys.

You can also set the probability a run ends on any given segment. This is
particularly good if the run can die at a certain point due to RNG, or it's
easy to take a death, and that isn't reflected in the splits. Apart from this
the possibility of a reset is not taken into account, because I have no ideas
for how to do this that I'm happy with.

You can also change the background and text colours, if you're so inclined.

## Contact

If you have any bug reports I would prefer they be reported on the GitHub page,
but this isn't required. My Discord is GenericMadScientist#5303, and you can
send me an e-mail at rayw1710@gmail.com. Discord will probably get you a quicker
response.