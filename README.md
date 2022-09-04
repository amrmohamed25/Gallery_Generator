# Gallery_Generator
A Complete project done during Bibliotheca Alexandrina Internship. The idea of the project is to help users that want to make a gallery using their own photos so that the images can be changed remotely and gallery be updated.
The project was divided into three phases:
<li>Unity Project</li>
<li>Flutter Web for data entry</li>
<li>Unity Webgl </li>

The first phase was done in 2 weeks. The main focus was to generate 3 layouts(Corridor,Square,Polygon) "The polygon was a bit challenging but eventually the equation was made it was L=2R/(TAN(2 * PI/(2 * n)))" and add some functionality for example: when the user press the photo he should be directed towards it and if he wants to unfocus he can press any area far from the photo. A corridor layout can be shown in the following video, Note that only 2 images were on the firebase storage and also zoom is a bit laggy

https://user-images.githubusercontent.com/55148866/185560858-d9200afe-1072-4d96-9e40-ce9d560328fa.mp4



The second phase was done in 2 days. Flutter web was choosen because the project needed to be done fast, also i was asked to use Firebase so flutter was a great option. Firebase realtime database was used because the API was available for unity. Firebase storage was used to store any texture or images.
Note that: Firebase firestore wasn't used because unity doesn't support working with it


https://user-images.githubusercontent.com/55148866/185560926-9e232716-348d-4ba3-92d6-feda1367d55c.mp4


The last phase was done in 3 days. This phase was challenging and there were some problems:
<li>Webgl doesn't support uploading local files</li> 
<li>The CORS policy (Cross-Origin Resource Sharing) took a while to be solved.</li> 
<li>There was a problem in decompressing the webgl build</li>
The solution for these problems respectively:
<li>Firebase storage was used to store images</li>
<li>The solution was to create a cors.json file and make it open for any url and to use gsutil (the command was: gsutil cors set yourFile.json gs://yourProject) which makes the firebase storage follow the CORS policy</li>
<li>All files inside the build were decompressed manually and the index.html was modified "removed the extension of .gz"</li>

Firebase Hosting was used to host the Gallery Generator and the url is https://gallery-3d1e5.web.app/.
