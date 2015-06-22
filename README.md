# WB3_JWT_Csharp
JWT library written for C#

<h3>Overview</h3>
<p>This library follows, with exceptions, the JSON WebToken standard as outlined in this webpage :<br /> http://self-issued.info/docs/draft-ietf-oauth-json-web-token.html</p>

Exceptions to the JSON Standard:
- Encryption Algorithm is TripleDES provided by the .NET Framework; this is the only selection available right now.

<h3>Create a WebToken</h3>
<p>Instantiate new Token object and fill with data. </p>
```
var t = new Token();

// super-secret signature key to sign token with
var secretForSignature = "SomeHardToCrackString";

// Set Header Attributes
t.Header.alg = WebToken.EncryptionAlgo.TripleDES.ToString();

// Set Body Attributes
t.Body.iss = "TheIssuer";
t.Body.aud = "TheAudience";
t.Bdy.iat = Utilities.NowInMilliseconds();        // Issued at the current time
t.Body.exp = Utilities.FutureInMilliseconds(36);   // Expires 36 hours from now
t.Body.scp = "public_profile,email,creator";      // Scope of access this; IS NOT a JWT Standard

// Convert C# Object to Signed JWT; you can now persist this string and use as you need for authorization.
var _JSONtoken = JWT.ConstructWebToken(t, secret);
```
<h3>Read a WebToken</h3>
<p>This will only read web tokens created with this library</p>
```
// the way your application retrieves a JWT String
var incomingJSONWebToken = YourMethodToGetAJWT();

// Your secret signature key used to sign the token
string signatureSecret = "SomeHardToCrackString";

try{

  // User DeconstructWebToken() to decrypt and convert incoming JWT Object into a C# object
  Token _JWT = JWT.DeconstructWebToken(incomingJSONWebToken , signatureSecret);
  
  // Get the informatition and do with it what you like
  Console.Write("The person who issued this JWT was " + _JWT.Body.iss + " and it was issued to " + _JWT.Body.aud);
  
} catch (Exception ex){

  if(typeof (System.Security.Cryptography.CryptographicException.CryptographicException) ){
    // Handle Error due to bad secret key.
    // This error means that the key supplied was not able to decrypt the JSON token
    
}
```

Two utility methods exist for DateTime data in milliseconds.
- Utilities.NowInMilliseconds()
- Utilities.FutureInMilliseconds(double hours)
 - takes an attribute of datatype double which is how many hours-from-now you want your future-time-in-milliseconds to be.
