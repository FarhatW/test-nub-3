import { NbMenuItem } from '@nebular/theme';
import {environment} from "../../environments/environment";

export const MENU_ITEMS: NbMenuItem[] = [

  {
    title: 'Dashboard',
    icon: 'nb-home',
    link: '/dashboard',
    home: true,
  },
  {
    title: 'CE',
    icon: 'ion-briefcase',
    link: '/ces',
    children: [
      {
        title: 'Liste des CE',
        link: '/ces/ce-list',
      },
      {
        title: 'Ajouter un CE',
        link: '/ces/ce-form/new',
      },
    ],
  },
  {
    title: 'Roles',
    icon: 'ion-ios-cog-outline',
    link: '/roles',

  },
  {
    title: 'Utilisateurs',
    icon: 'ion-ios-people-outline',
    link: '/users',
    children: [
      {
        title: 'Liste des utilisateurs',
        link: '/users/users-list',
      },
      {
        title: 'Ajouter un utilisateur',
        link: '/users/users-form/new',
      },
    ]
  },
  {
    title: 'Produits',
    icon: 'ion-ios-game-controller-a-outline',
    link: '/products',
    children: [
      {
        title: 'Liste des Produits',
        link: '/products/product-list',
      }
    ]
  },
  {
    title: 'Fournisseurs',
    icon: 'ion-android-bus',
    link: '/suppliers',
    children: [
      {
        title: 'Liste des fournisseurs',
        link: '/suppliers/list',
      }
    ]
  },
  {
    title: 'Fiches de Collectivités',
    icon: 'ion-clipboard',
    link: '/pintelsheets',
    children: [
      {
        title: 'Liste des Fiches de Collectivités',
        link: '/pintelsheets/list',
      }
    ]
  },
  {
    title: 'Gestion des Images & Fichiers',
    icon: 'ion-ios-folder-outline',
    link: '/files',
  },
  {
    title: 'UI Features',
    icon: 'nb-keypad',
    link: '/ui-features',
    hidden: environment.production,
    children: [
      {
        title: 'Buttons',
        link: '/ui-features/buttons',
      },
      {
        title: 'Grid',
        link: '/ui-features/grid',
      },
      {
        title: 'Icons',
        link: '/ui-features/icons',
      },
      {
        title: 'Modals',
        link: '/ui-features/modals',
      },
      {
        title: 'Typography',
        link: '/ui-features/typography',
      },
      {
        title: 'Animated Searches',
        link: '/ui-features/search-fields',
      },
      {
        title: 'Tabs',
        link: '/ui-features/tabs',
      },
    ],
  },
  {
    title: 'Forms',
    icon: 'nb-compose',
    hidden: environment.production,
    children: [
      {
        title: 'Form Inputs',
        link: '/forms/inputs',
      },
      {
        title: 'Form Layouts',
        link: '/forms/layouts',
      },
    ],
  },
  {
    title: 'Components',
    icon: 'nb-gear',
    hidden: environment.production,
    children: [
      {
        title: 'Tree',
        link: '/components/tree',
      }, {
        title: 'Notifications',
        link: '/components/notifications',
      },
    ],
  },
  {
    title: 'Maps',
    icon: 'nb-location',
    hidden: environment.production,

    children: [
      {
        title: 'Google Maps',
        link: '/maps/gmaps',
      },
      {
        title: 'Leaflet Maps',
        link: '/maps/leaflet',
      },
      {
        title: 'Bubble Maps',
        link: '/maps/bubble',
      },
    ],
  },
  {
    title: 'Charts',
    icon: 'nb-bar-chart',
    hidden: environment.production,

    children: [
      {
        title: 'Echarts',
        link: '/charts/echarts',
      },
      {
        title: 'Charts.js',
        link: '/charts/chartjs',
      },
      {
        title: 'D3',
        link: '/charts/d3',
      },
    ],
  },
  {
    title: 'Editors',
    icon: 'nb-title',
    hidden: environment.production,

    children: [
      {
        title: 'TinyMCE',
        link: '/editors/tinymce',
      },
      {
        title: 'CKEditor',
        link: '/editors/ckeditor',
      },
    ],
  },
  {
    title: 'Tables',
    icon: 'nb-tables',
    hidden: environment.production,
    children: [
      {
        title: 'Smart Table',
        link: '/tables/smart-table',
      },
    ],
  },
  {
    title: 'Auth',
    icon: 'nb-locked',
    hidden: environment.production,
    children: [
      {
        title: 'Login',
        link: '/auth/login',
      },
      {
        title: 'Register',
        link: '/auth/register',
      },
      {
        title: 'Request Password',
        link: '/auth/request-password',
      },
      {
        title: 'Reset Password',
        link: '/auth/reset-password',
      },
    ],
  },
];
