#!/bin/bash
pandoc -s --metadata pagetitle="The LanguageExt Tutorial" --highlight-style tango --filter pandoc-include-code tutorial.md -o tutorial1.html
