class ResourcesModel{
  List<String> myResources=[];

  ResourcesModel();

  ResourcesModel.fromJson(var json){
    json.forEach((element) {
      print(element);
      myResources.add(element);
    });
  }

}