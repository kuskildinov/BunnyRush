mergeInto(LibraryManager.library,
  {
     ReadyGameplayInternal: function () {      
     ysdk.features.LoadingAPI.ready();
    },

    SaveExtern: function (data) {
      var dataString = UTF8ToString(data);
      var myobj = JSON.parse(dataString);
      player.setData(myobj);
    },

    LoadExtern: function () {
      player.getData().then(data => {
        const myJSON = JSON.stringify(data);
        myGameInstance.SendMessage('SaveRoot', 'LoadData', myJSON);
      });
    },

    GetLang: function () {
      var lang = ysdk.environment.i18n.lang;
      var bufferSize = lengthBytesUTF8(lang) + 1;
      var buffer = _malloc(bufferSize);
      stringToUTF8(lang, buffer, bufferSize);
      return buffer;
    },

    CheckIsInited: function () {
      YaGames.init().then(() => {
        myGameInstance.SendMessage('MyYandex', 'InitializeGame');
      });
    },

    StartGameplayInternal: function () {
      ysdk.features.GameplayAPI.start();
    },

    StopGameplayInternal: function () {
      ysdk.features.GameplayAPI.stop()
    },

    ShowAdInternal: function () {      
      ysdk.adv.showFullscreenAdv({
        callbacks: {
          onOpen : () => {
            // Действие после открытия рекламы.
            myGameInstance.SendMessage('AdvRoot', 'OnAdvOpend');
          },
          onClose: (wasShown) => {
            // Действие после закрытия рекламы.
            myGameInstance.SendMessage('AdvRoot', 'OnAdvClosed');
          },
          onError: () => {
            // Действие в случае ошибки.
           
          },
        }
      });
    },

    ShowRewardVideoAdInternal: function () {
      ysdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: () => {            
            myGameInstance.SendMessage('AdvRoot', 'OnAdvOpend');
          },
          onRewarded: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnAdvRewarded');
          },
          onClose: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnAdvClosed');
          },
          onError: (e) => {
            console.log('Error while open video ad:', e);
          },
        },
      });
    },

    ShowRestartRewardAdInternal: function () {
      ysdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: () => {            
            myGameInstance.SendMessage('AdvRoot', 'OnAdvOpend');
          },
          onRewarded: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnRestartAdvRewarded');
          },
          onClose: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnAdvClosed');
          },
          onError: (e) => {
            console.log('Error while open video ad:', e);
          },
        },
      });
    },
    ShowDoubleCoinRewardAdInternal: function () {
      ysdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: () => {            
            myGameInstance.SendMessage('AdvRoot', 'OnAdvOpend');
          },
          onRewarded: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnDoubleCoinAdvRewarded');
          },
          onClose: () => {           
            myGameInstance.SendMessage('AdvRoot', 'OnAdvClosed');
          },
          onError: (e) => {
            console.log('Error while open video ad:', e);
          },
        },
      });
    },

    CheckGameFocusInternal: function () {
      window.onfocus = function () {
        myGameInstance.SendMessage('MyYandex', 'GameWindowOnFocus');
      }

      window.onblur = function () {
        myGameInstance.SendMessage('MyYandex', 'GameWindowOnBlur');
      }
    },

    SetToLeaderBoard: function (value) {
      ysdk.getLeaderboards()
        .then(lb => {
          lb.setLeaderboardScore('BestRunner', value);
        });
    },

    GetBestPlayerName: function () {
      ysdk.getLeaderboards()
  .then(lb => {        
    lb.getLeaderboardEntries('leaderboard2021', { quantityTop: 1 })
      .then(res => myGameInstance.SendMessage('BestPlayerScoreView', 'SetBestPlayerName', res.publicName));
  });
      
    },

  });
