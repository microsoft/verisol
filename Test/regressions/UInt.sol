pragma solidity >=0.4.24 <0.6.0;


// Test uint implementation
contract UintTest {
  uint x;
  uint256 x256;
  uint16 x16;
  
  constructor () public {
  }

  function testCall(uint p) public  {
    assert(p>=0);
    assert (x>=0);
    assert (x16>=0);
    assert (x256>=0);
    x++;
    x16++;
    x256 = x+x16;
    uint res = testRet(p);
    assert(res>0);
  }
  function testRet(uint p) public  returns (uint r)
  {
    assert (x>=0);
    assert (x16>=0);
    assert (x256>=0);

    assert(p>=0);
    r = p + 1;
    assert(r>0);
  }

  function testNonNegative() public 
  {
      x--;
      // should not fail
      if(x<0)
        assert(false);
  }


}
