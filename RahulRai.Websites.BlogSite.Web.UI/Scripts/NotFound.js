﻿var tl = new Array(
"Hate to tell you this, but that page ain\'t here.",
"Seriously. It\'s not.",
"404 baby. 40-freakin\'-4.",
"I even tried multi,",
"which is much more than I do for most folks.",
"Most times, I just zap up a blank page",
"and give the old 404 rap,",
"But you, you\'re special.",
"And, if I could, I\'d run off right now",
"and find the page you wanted.",
"Or, even better, run off with you",
"and screw the stupid page.",
"God, you\'re beautiful.",
"Your eyes are just like vapid pools of fire.",
"Lipid! Not vapid, lipid!",
"Sorry. no, it\'s LIMPID! Limpid pools.",
"Of fire.",
"Your eyes are, I mean.",
"Sorry about that,",
"but I\'m just not used to talking to such a gorgeous Web surfer.",
"I\'m all tongue-tied.",
"See, you smiled. God, your smile is so great.",
"Just one smile from you, and I\'d be able to go for weeks.",
"I mean, don\'t get me wrong...",
"I meet a lot of folks on this job.",
"And sure, there are some beauties,",
"but there\'s no one like you.",
"*sigh*",
"So whadya think?",
"Is it possible for a hard-working, generally bug-free Web server",
"and a beautiful Web surfer to find love",
"in this crazy, mixed-up world?",
"You do? I do too!",
"OK, OK, I\'m sorry.",
"Moving too fast. Sorry.",
"Oh, you don\'t have to go yet!",
"Look, I\'m sorry things got a little screwy.",
"I just want to start over.",
"I promise I\'ll just serve the page, nothing more.",
"But if it\'s OK with you, can I still admire you from a distance?",
"*sigh*"
);

var speed = 80;
var index = 0; text_pos = 0;
var str_length = tl[0].length;
var contents, row;

function type_text() {
    contents = '';
    row = Math.max(0, index - 6);
    while (row < index)
        contents += tl[row++] + '\r\n';
    document.textform.elements[0].value = contents + tl[index].substring(0, text_pos) + "_";
    if (text_pos++ == str_length) {
        text_pos = 0;
        index++;
        if (index != tl.length) {
            str_length = tl[index].length;
            setTimeout("type_text()", 1500);
        }
    } else
        setTimeout("type_text()", speed);
}
function MM_callJS(jsStr) {
    //v2.0
    return eval(jsStr)
}