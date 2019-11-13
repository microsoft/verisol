pragma solidity >=0.4.24 <0.6.0;


// Test uint implementation
contract UintTest {
  uint x;
  uint256 x256;
  uint16 x16;
  uint[] arrayUint;
  mapping (int => uint) mapUint;
  uint[][] nestedArrayUint;

  constructor () public {
  }

  // pasing  uint[][] array is not possible unless we add pragma experimental ABIEncoderV2
  function testCall(uint p,  uint[] memory array2) public  {
    //require(arrayUint.length>0,"Empty array");
    //require(nestedArrayUint.length>0,"Empty array");

    assert(p>=0);
    assert (x>=0);
    assert (x16>=0);
    assert (x256>=0);
    x++;
    x16++;
    x256 = x+x16;
    assert(arrayUint[0]>=0);
    assert(nestedArrayUint[0][0]>=0);
    // If I never assign a value then mapUint[1] == 0
    mapUint[1] = array2[0];
    assert(mapUint[1]>=0);

    uint res = testRet(p);
    assert(res>0);
  }
  function interFunc(mapping (int=>uint) storage map2) internal
  {
    assert(map2[1]>=1);
  }
  function testRet(uint p) public  returns (uint r)
  {
    assert (x>=0);
    assert (x16>=0);
    assert (x256>=0);

    assert(p>=0);
    r = p + 1;
    assert(r>0);

    assert(mapUint[1]>=0);
    mapUint[1]++;
    interFunc(mapUint);
  }

  function testNonNegative() public
  {
      x--;
      // should not fail
      if(x<0)
        assert(false);
  }


}
