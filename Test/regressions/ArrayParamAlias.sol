pragma solidity >=0.4.24 <0.6.0;

// array variables should not alias with array params or local variables

contract ArrayLength {
    uint[] a;

    constructor (uint[] d) 
    {
       require(d.length == 2);
       a.push(11);
       assert(d.length == 2); //should not change due to push

    }

    function test(uint[] b) {
     
       uint[] c;
  
       b[0] = 33;
       assert (a[0] == 11);

       c.push(33);
       assert (a.length == 1); // fails as c can alias
       assert (c.length == 1);

       require(false); //only execute this once
    }

}
