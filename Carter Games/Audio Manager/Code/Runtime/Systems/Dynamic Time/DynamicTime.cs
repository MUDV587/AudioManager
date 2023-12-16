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
using UnityEngine;

namespace CarterGames.Assets.AudioManager
{
    /// <summary>
    /// Defines a dynamic time point.
    /// </summary>
    [Serializable]
    public sealed class DynamicTime
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        public float time;
        [Range(0f, .5f)] public float threshold;
        [SerializeField] private DynamicTimeOption option;
        [SerializeField] private int tabPos;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Constructors
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Creates a new dynamic time with default settings.
        /// </summary>
        public DynamicTime() { }

        
        /// <summary>
        /// Creates a new dynamic time with the params entered.
        /// </summary>
        /// <param name="time">The time to start at.</param>
        /// <param name="sample">The sample to start from.</param>
        /// <param name="threshold">The threshold to detect audio starting at.</param>
        public DynamicTime(float time, int sample, float threshold)
        {
            this.time = time;
            this.threshold = threshold;
        }
    }
}