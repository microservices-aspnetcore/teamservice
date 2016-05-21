set -e
curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.31.1/install.sh | bash
source $HOME/.nvm/nvm.sh
nvm install 4
nvm use 4
npm install -g bower
npm install -g gulp          
echo '{ "allow_root": true }' > $HOME/.bowerrc
