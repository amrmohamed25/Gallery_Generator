class TextureModel{
   String? albedo;
   String? ao;
   String? normal;
   String? metallic;
   String? height;
   String? roughness;


  TextureModel.fromJson(Map json){
    albedo=json["albedo"];
    height=json["height"];
    ao=json["ao"];
    normal=json["normal"];
    metallic=json["metallic"];
    roughness=json["roughness"];
  }
  setMap(){
    return {"height":height,"albedo":albedo,"ao":ao,"normal":normal,"metallic":metallic,"roughness":roughness};
  }
}