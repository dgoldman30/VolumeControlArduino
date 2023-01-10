#define VERT_PIN A0
#define HORZ_PIN A1
#define SEL_PIN 2
#define LED_BUILTIN 13
#define OPENMIC_PIN 12
#include <Keyboard.h>

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(OPENMIC_PIN, OUTPUT);
  pinMode(VERT_PIN, INPUT);
  pinMode(HORZ_PIN, INPUT);
  pinMode(SEL_PIN, INPUT_PULLUP);
  Serial.begin(9600);
}

boolean mute = false;
void loop() {
  String ret = "";
  if (mute == false) {
    digitalWrite(OPENMIC_PIN, HIGH);
    digitalWrite(LED_BUILTIN, LOW);
  } else {
    digitalWrite(OPENMIC_PIN, LOW);
    digitalWrite(LED_BUILTIN, HIGH);
  }

  // int val = analogRead(VERT_PIN);
  // int ret = 0;
  // if (val > 700 && mute == false) {
  //   ret = 1;
  // }
  // else if (val < 300 && mute == false)
  // {
  //   ret = -1;
  // }
  // else if (mute == true) {
  //   ret = -100;
  // }
  // if (digitalRead(SEL_PIN) == LOW && mute == true) {
  //   mute = false;
  // }
  // else if (digitalRead(SEL_PIN) == LOW && mute == false){
  //   mute = true;
  // }
  // Serial.println(ret);
  // delay(200); //this delay is vital. Thank you to "09" who pointed this out to me! :)
  // INSTEAD WELL TRACK X AND Y SERIAL PRINT BASED ON ACTION
  if (digitalRead(SEL_PIN) == LOW && mute == true) {
    mute = false;
  } else if (digitalRead(SEL_PIN) == LOW && mute == false) {
    mute = true;
  }

  int xval = analogRead(HORZ_PIN);
  int yval = analogRead(VERT_PIN);

  if (yval > 700 && mute == false) {
    ret = "V+";
  } else if (yval < 300 && mute == false) {
    ret = "V-";
  } else if (mute) {
    ret = "MUTE";
  }
   else if (xval > 700 && mute == false) {
    ret = "M-";
  } else if (xval < 300 && mute == false) {
    ret = "M+";
  }
  Serial.println(ret);
  delay(200);
}
