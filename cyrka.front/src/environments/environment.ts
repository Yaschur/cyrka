// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
	production: false,
	cyrkaApi: {
		baseUrl: 'http://localhost:5000',
	},
	auth: {
		clientID: 'rDRpZgNYpPWQKnd2GeOJo3fe284vZQJg',
		domain: 'cyrka-dev.eu.auth0.com',
		audience: 'http://localhost:5000',
		redirect: 'http://localhost:4200/callback',
		scope: 'openid profile email',
	},
};
