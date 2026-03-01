import packageJson from '../../package.json';
const HOST = 'http://localhost:5000/api';
export const enviroments = {
  context: 'develop',
  API_PUBLIC: HOST + '/',
  version: packageJson.version,
};
