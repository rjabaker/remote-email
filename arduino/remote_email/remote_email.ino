int inputA = 0;
int inputPinA = 5;
boolean channelAOpen = true;

int inputB = 0;
int inputPinB = 4;
boolean channelBOpen = true;

void setup(){
  Serial.begin(9600);
}

void loop(){
 readChannel(&inputA, &channelAOpen, inputPinA);
 // readChannel(&inputB, &channelBOpen, inputPinB);
}

void readChannel(int *input, boolean *channelOpen, int inputPin){
  *input = analogRead(inputPin);
  if(*input > 1020 && *channelOpen){
   // Serial.println(inputPin);
   // Serial.println(*input);
   Serial.write(inputPin);
   *channelOpen = false;
   // Serial.println("Closed");
 }
 else if(*input < 100){
   // Must wait for voltage to dissipate to minimum range after channel is opened.
   // if(*channelOpen == false) Serial.println("Open");
  *channelOpen = true; 
 }
}
