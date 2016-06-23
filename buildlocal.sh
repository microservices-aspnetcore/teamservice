rm -rf _builds _steps _projects _cache _temp
wercker build --git-domain github.com --git-owner microservices-aspnetcore --git-repository teamservice
rm -rf _builds _steps _projects _cache _temp