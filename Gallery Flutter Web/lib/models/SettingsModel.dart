
class SettingsModel{
  late double height;
  late double radius;


  SettingsModel(this.height,this.radius);
  SettingsModel.fromJson(Map json){
    height=json["height"];

    radius=json["radius"];
  }
  setMap(){
    return {"height":height,"radius":radius};
  }
}