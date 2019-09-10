

String inputString = "";
boolean stringComplete = false;

void setup(){
  udp_setup();
  Serial.begin(9600);
  inputString.reserve(200);
}

void loop(){
  udp_check();
  if(stringComplete){
    udp_sends(inputString);
    inputString = "";
    stringComplete = false;
  }
}

void serialEvent(){
  while(Serial.available()){
    char inChar = (char)Serial.read();
    inputString += inChar;
    if(inChar == '\n'){
      stringComplete = true;
    }
  }
}
