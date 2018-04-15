\ https://www.reddit.com/r/dailyprogrammer/comments/879u8b/20180326_challenge_355_easy_alphabet_cipher/

: ?between ( -- )
  dup rot <= -rot swap >= AND
;
: ?upperletter ( -- )
  [char] A [char] Z rot ?between 
;
: ?lowerletter ( c1 -- t|f )
  [char] a [char] z rot ?between
;

: >lowercase ( -- )
  dup ?upperletter if [char] a [char] A - + else
  dup ?lowerletter if else 0
  then then
;

: >alphanum ( -- )
  >lowercase dup 0= if else [char] a - 1 + then
;

: alphanum> ( -- )
  1 - [char] a +
;

: >encoded  ( -- )
  >alphanum 1-      \ convert to zero based numbers
  swap >alphanum 1- 
  + 26 mod 1+ alphanum>
;

: >decoded ( -- )
  - 1 - 26 mod
;

: >encodedphrase
  0 do
    dup i + c@      \ ith char of phrase to encode
    >r              \ save on return stack 
    -rot 2dup       \ bring key length and address to front
    i swap mod + c@ \ get correct key char
    r>              \ grab back from return stack
    >encoded emit   \ encode and display
    rot             \ bring phrase addr back to top of stack 
  loop 
  drop drop drop    \ cleanup stack
;

: test
  s" snitch" s" thepackagehasbeendelivered"  >encodedphrase cr
  s" train" s" murderontheorientexpress" >encodedphrase cr
  s" garden" s" themolessnuckintothegardenlastnight" >encodedphrase cr
;
