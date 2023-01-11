#define VERT_PIN A0
#define HORZ_PIN A1
#define SEL_PIN 2
#define LED_BUILTIN 13
#define OPENMIC_PIN 12
#include <ezButton.h>

ezButton button(2); 

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
     button.loop(); 
  String ret = "";
  if (button.isReleased()) {
    mute = !mute;
    if (mute) {
      ret = "MU";
    }
    else {
      ret = "UN";
    }
  }
  if (mute == false) {
    digitalWrite(OPENMIC_PIN, HIGH);
    digitalWrite(LED_BUILTIN, LOW);
  } else {
    digitalWrite(OPENMIC_PIN, LOW);
    digitalWrite(LED_BUILTIN, HIGH);
  }
  int xval = analogRead(HORZ_PIN);
  int yval = analogRead(VERT_PIN);

  if (yval > 700 && mute == false) {
    ret = "V+";
  } else if (yval < 300 && mute == false) {
    ret = "V-";
  } 
   else if (xval > 700 && mute == false) {
    ret = "M-";
  } else if (xval < 300 && mute == false) {
    ret = "M+";
  }
  Serial.println(ret);
   delay(200);

}
