# Tic-Tac-Toe Player

Created in C# by Blair Palmerlee

## Overview

TTT Player is a console based application built as an excercise in simple computer strategy. This runs in the terminal of Linux, Mac, or Windows computers. 

In this terminal-based game, the player is prompted to either place their piece first or second. Play then commenses on a Tic Tac Toe board where columns are numbered 1-3 and rows are lettered A-B.

Play ends with a win, lose, or tie. 

## Computer choices

The computer player choices are made based on a virtual heatmap generated every time a move is made. Based on proximity to taken spaces and if a space would trigger a win or loss, the program add value to spaces. When the computer takes its turn it selects the highest value space on the board.

## How to use

To run this program:


1. Unzip the file.

2. Open the terminal in the extracted directory.

3. Run TTTPlayer using your systems equivolent run command: 

'./TTTPlayer' - Linux

'.\TTTPlayer' - Windows
