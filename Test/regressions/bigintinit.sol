pragma solidity >=0.4.24 <0.6.0;

// testing for issue #189
contract A { 
    int x = 0x111111111111;
    address y = address(0x111111111111);
    function Foo(int a) public {
         assert (x == 18764998447377);
         assert (y == address(18764998447377));
    }
}