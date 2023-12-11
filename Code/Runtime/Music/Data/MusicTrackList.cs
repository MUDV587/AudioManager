﻿/*
 * Copyright (c) 2018-Present Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CarterGames.Assets.AudioManager
{
    /// <summary>
    /// Contains all the data for a track list of music.
    /// </summary>
    [Serializable]
    public sealed class MusicTrackList
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        [SerializeField] private string listKey;
        [SerializeField] private List<MusicTrack> tracks;
        [SerializeField] private TrackType trackListType;
        [SerializeField] private bool loop = true;
        [SerializeField] private bool shuffle = false;

        [SerializeField] private bool useCustomTransitions;
        [SerializeField] private MusicTransition startingTransition;
        [SerializeField] private float startingTransitionDuration = 1f;
        [SerializeField] private MusicTransition switchTrackTransition;
        [SerializeField] private float switchingTransitionDuration = 1f;

        private IMusicPlayer playerRef;
        private IMusicTransition startingTransitionRef;
        private IMusicTransition switchTrackTransitionRef;
        private MusicTransition defaultStart = MusicTransition.Fade;
        private MusicTransition defaultSwitching = MusicTransition.CrossFade;
        
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Gets the type of track list this list is.
        /// </summary>
        public TrackType TrackListType => trackListType;
        
        
        /// <summary>
        /// Gets if the track list should loop when completed.
        /// </summary>
        public bool TrackListLoops => loop;
        
        
        /// <summary>
        /// Gets if the track list should play in a shuffled order or not.
        /// </summary>
        public bool PlayListShuffled => shuffle;

        
        /// <summary>
        /// Gets the starting transition for the track list.
        /// </summary>
        public IMusicTransition StartingTransition
        {
            get
            {
                if (startingTransitionRef != null) return startingTransitionRef;
                
                startingTransitionRef = useCustomTransitions 
                    ? MusicManager.GetTransitionFromEnum(startingTransition, startingTransitionDuration)
                    : MusicManager.GetTransitionFromEnum(defaultStart, 1f);
                
                return startingTransitionRef;
            }
        }
        
        
        /// <summary>
        /// Gets the switching transition for the track list.
        /// </summary>
        public IMusicTransition SwitchingTransition
        {
            get
            {
                if (switchTrackTransitionRef != null) return switchTrackTransitionRef;
                
                switchTrackTransitionRef = useCustomTransitions 
                    ? MusicManager.GetTransitionFromEnum(switchTrackTransition, switchingTransitionDuration)
                    : MusicManager.GetTransitionFromEnum(defaultSwitching, 1f);
                
                return switchTrackTransitionRef;
            }
        }

        public string ListKey => listKey;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// Gets if a track key exists in the track list.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>If the key was found or not.</returns>
        public bool HasTrack(string key)
        {
            return tracks.Any(t => t.ClipKey.Equals(key));
        }
        
        
        /// <summary>
        /// Gets the track at the entered index.
        /// </summary>
        /// <param name="index">The index to get.</param>
        /// <returns>The audio clip found.</returns>
        public AudioClip GetTrack(int index)
        {
            return AssetAccessor.GetAsset<AudioLibrary>().GetData(tracks[index].ClipKey).value;
        }
        
        
        /// <summary>
        /// Gets the track at the entered key.
        /// </summary>
        /// <param name="key">The key to get.</param>
        /// <returns>The audio clip found.</returns>
        public AudioClip GetTrack(string key)
        {
            return AssetAccessor.GetAsset<AudioLibrary>().GetData(key).value;
        }


        /// <summary>
        /// Gets the start time for the track of the entered index.
        /// </summary>
        /// <param name="index">The index to get.</param>
        /// <returns>The start time found.</returns>
        public float GetStartTime(int index = 0)
        {
            return tracks[index].StartTime;
        }
        
        
        /// <summary>
        /// Gets the start time for the track of the entered key.
        /// </summary>
        /// <param name="key">The key to get.</param>
        /// <returns>The start time found.</returns>
        public float GetStartTime(string key)
        {
            var data = tracks.FirstOrDefault(t => t.ClipKey.Equals(key));

            if (data != null)
            {
                return data.StartTime;
            }

            return 0f;
        }
        
        
        /// <summary>
        /// Gets the end time for the track of the entered index.
        /// </summary>
        /// <param name="index">The index to get.</param>
        /// <returns>The end time found.</returns>
        public float GetEndTime(int index = 0)
        {
            return tracks[index].EndTime;
        }
        
        
        /// <summary>
        /// Gets the end time for the track of the entered key.
        /// </summary>
        /// <param name="key">The key to get.</param>
        /// <returns>The end time found.</returns>
        public float GetEndTime(string key)
        {
            var data = tracks.FirstOrDefault(t => t.ClipKey.Equals(key));

            if (data != null)
            {
                return data.EndTime;
            }

            return 0f;
        }
        
        
        /// <summary>
        /// Gets all the tracks in the track list as audio clips.
        /// </summary>
        /// <returns>The track list as audio clips.</returns>
        public List<MusicTrack> GetTracksRaw()
        {
            return tracks;
        }
        
        
        /// <summary>
        /// Gets all the tracks in the track list as audio clips.
        /// </summary>
        /// <returns>The track list as audio clips.</returns>
        public List<AudioClip> GetTracks()
        {
            var tracks = new List<AudioClip>();

            foreach (var id in this.tracks)
            {
                tracks.Add(AssetAccessor.GetAsset<AudioLibrary>().GetData(id.ClipKey).value);
            }

            return tracks;
        }

        
        /// <summary>
        /// Assigns the music player or makes a new instance if needed.
        /// </summary>
        /// <returns>The player for the track list to use.</returns>
        public IMusicPlayer GetMusicPlayer()
        {
            if (playerRef != null) return playerRef;
            
            switch (trackListType)
            {
                case TrackType.Single:
                    playerRef = new SingleMusicTrackPlayer();
                    break;
                case TrackType.Playlist:
                    playerRef = new PlaylistMusicTrackPlayer();
                    break;
                default:
                    break;
            }

            return playerRef;
        }
    }
}