using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Reflection;
using System;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

< !DOCTYPE html >
< html lang = "en" >
< head >
  < meta charset = "UTF-8" >
  < meta name = "viewport" content = "width=device-width, initial-scale=1.0" >
  < title > AP Gov Wordle</title>
  <link rel = "stylesheet" href= "style.css" >
</ head >
< body >
  < header >
    < h1 > AP Gov Wordle</h1>
    <p>Guess the AP Government and Politics word in 6 tries!</p>
  </header>
  <main>
    <div id = "board" ></ div >
    < div id= "keyboard" ></ div >
  </ main >
  < script src= "script.js" ></ script >
</ body >
</ html >

body {
  font-family: Arial, sans-serif;
background - color: #f5f5f5;
  margin: 0;
padding: 0;
display: flex;
flex - direction: column;
align - items: center;
}

header {
  margin: 20px;
text - align: center;
}

#board {
  display: grid;
grid - template - columns: repeat(5, 50px);
grid - gap: 5px;
margin - top: 20px;
}

.cell {
  width: 50px;
height: 50px;
border: 2px solid #ccc;
  text-align: center;
font - size: 24px;
font - weight: bold;
line - height: 50px;
text - transform: uppercase;
background - color: white;
}

.cell.correct {
    background - color: green;
color: white;
}

.cell.present {
    background - color: orange;
color: white;
}

.cell.absent {
    background - color: #ccc;
  color: white;
}

# keyboard {
display: flex;
flex - wrap: wrap;
justify - content: center;
margin - top: 20px;
}

.key {
  width: 40px;
height: 40px;
margin: 5px;
text - align: center;
line - height: 40px;
font - size: 18px;
font - weight: bold;
background - color: #ddd;
  border: none;
cursor: pointer;
}

.key.correct {
    background - color: green;
color: white;
}

.key.present {
    background - color: orange;
color: white;
}

.key.absent {
    background - color: #ccc;
  color: white;
}

const words = ['vetos', 'votes', 'party', 'judge', 'court', 'civics'];
const targetWord = words[Math.floor(Math.random() * words.length)];
const maxAttempts = 6;

let currentAttempt = 0;
let currentGuess = '';

const board = document.getElementById('board');
const keyboard = document.getElementById('keyboard');

// Initialize the game board
for (let i = 0; i < maxAttempts * 5; i++)
{
    const cell = document.createElement('div');
    cell.classList.add('cell');
    board.appendChild(cell);
}

// Initialize the keyboard
const letters = 'abcdefghijklmnopqrstuvwxyz';
for (const letter of letters) {
  const key = document.createElement('button');
key.classList.add('key');
key.innerText = letter;
key.addEventListener('click', () => handleKeyPress(letter));
keyboard.appendChild(key);
}

function handleKeyPress(letter)
{
    if (currentGuess.length < 5)
    {
        currentGuess += letter;
        updateBoard();
    }
}

function updateBoard()
{
    const cells = document.querySelectorAll('.cell');
    const start = currentAttempt * 5;
    for (let i = 0; i < 5; i++)
    {
        cells[start + i].innerText = currentGuess[i] || '';
    }
}

document.addEventListener('keydown', (event) => {
    const letter = event.key.toLowerCase();
    if (/ ^ [a - z]$/.test(letter)) {
        handleKeyPress(letter);
    } else if (event.key === 'Enter') {
        submitGuess();
    } else if (event.key === 'Backspace') {
        handleBackspace();
    }
});

function handleBackspace()
{
    currentGuess = currentGuess.slice(0, -1);
    updateBoard();
}

function submitGuess()
{
    if (currentGuess.length !== 5) return;

    const cells = document.querySelectorAll('.cell');
    const start = currentAttempt * 5;

    for (let i = 0; i < 5; i++)
    {
        const cell = cells[start + i];
        const letter = currentGuess[i];
        if (letter === targetWord[i])
        {
            cell.classList.add('correct');
            updateKey(letter, 'correct');
        }
        else if (targetWord.includes(letter))
        {
            cell.classList.add('present');
            updateKey(letter, 'present');
        }
        else
        {
            cell.classList.add('absent');
            updateKey(letter, 'absent');
        }
    }

    if (currentGuess === targetWord)
    {
        setTimeout(() => alert('You Win!'), 100);
        return;
    }

    currentAttempt++;
    currentGuess = '';

    if (currentAttempt === maxAttempts)
    {
        setTimeout(() => alert(`You Lose! The word was ${ targetWord}`), 100);
    }
}

function updateKey(letter, status)
{
    const keys = document.querySelectorAll('.key');
    for (const key of keys) {
        if (key.innerText === letter)
        {
            key.classList.add(status);
        }
    }
}
