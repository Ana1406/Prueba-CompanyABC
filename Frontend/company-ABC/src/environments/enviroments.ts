import packageJson from '../../package.json';
const HOST = 'https://jsonplaceholder.typicode.com';
const HOST_PHOTOS = 'https://fakestoreapi.com';
export const enviroments = {
  context: 'develop',
  API_PUBLIC: HOST + '/',
  API_PHOTOS: HOST_PHOTOS + '/',
  version: packageJson.version,
};
